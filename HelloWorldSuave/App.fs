namespace HelloWorldSuave

open System
open System.Net
open Suave
open Suave.Web
open Suave.Types
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful

module App =
    
    let config =
        let ip = IPAddress.Parse "0.0.0.0"
        { defaultConfig with
            logger = Logging.Loggers.saneDefaultsFor Logging.LogLevel.Verbose
            bindings = [ HttpBinding.mk HTTP ip 9001us ] }
            
    [<EntryPoint>]
    let main args =
        
        let app =
            choose [
                GET >>= choose [
                    path "/" >>= OK "Hello world"
                    path "/hello" >>=  OK "Get Hello!"
                ]
                POST >>= choose [
                    path "/hello" >>= OK "Post Hello!"
                ]
            ]

        startWebServer config app
        0