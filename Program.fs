namespace UserScripts.Filmow

open System
open Browser
open Fable.Core
open Fable.Core.JsInterop

module JQuery = 
    [<Emit("window['jQuery']($0)")>]
    let select : selector:string -> obj = jsNative
    
module Filmow =
    ()

module Program =
    printfn "UserScripts.Filmow loaded (Program.fs)"
    
    importAll "./index.scss"
    
    let movies = JQuery.select ".list_items_wrapper .movie_list_item"
    
    if movies?length > 0 then
        ".list_items_wrapper"
        |> JQuery.select
        |> fun x -> x?prepend """
            <div>
                <a href="#" onclick="window.randomList()">Random</a>
                <br/>
                "<span id="random_result"></span>"
            </div> """
            
        JS.setInterval (fun () ->
            
            let scrollButton = JQuery.select ".btn-lists-infinite-scroll"
            
            printfn "ATTR: %A" (scrollButton?attr "disabled")
            
            if scrollButton?length > 0 && scrollButton?attr "disabled" <> "disabled" then
                scrollButton?click ()
                
        ) 500 |> ignore
        
        Dom.window?randomList <- fun () ->
            let movies = JQuery.select ".list_items_wrapper .movie_list_item"
            
            let scrollButton = JQuery.select ".btn-lists-infinite-scroll"
            
            if scrollButton?length > 0 && scrollButton?attr "disabled" <> "disabled" then
                "#random_result"
                |> JQuery.select
                |> fun x -> x?html "Load all movies"
            else
                printfn "movie len: %A" movies?length
                
                let random = Random().Next (0, movies?length - 1)
                
                printfn "random: %A" random
                
                let movieName =
                    ".movie_list_item"
                    |> JQuery.select
                    |> fun x -> x?eq(random)?find("img")?attr("alt")
                    |> fun x -> x?substring (1, 2)
                
                "#random_result"
                |> JQuery.select
                |> fun x -> x?html (sprintf "%d - %s" random movieName)
    

