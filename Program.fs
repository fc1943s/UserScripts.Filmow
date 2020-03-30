namespace UserScripts.Filmow

open System
open Browser
open Fable.Core
open Fable.Core.JsInterop

module Core =
    let setInterval ms fn =
        let mutable _token = 0
            
        _token <- JS.setInterval (fun () ->
            fun () -> JS.clearInterval _token
            |> fn
        ) ms

module JQuery =
    type Element =
        { Raw: obj }
        
    let rawSelect selector : obj =
        Dom.window?jQuery selector
        
    let setHtml html element =
        element?html html
        
        
    let trySelect<'T> =
        rawSelect 
        >> fun items ->
            if items?length = 0
            then None
            else Some (items?eq 0)
            
    let selectItems<'T> =
        rawSelect
        >> fun items ->
            seq {
                for i in 0 .. items?length - 1 do
                    items?eq i
            }
        
    let ready fn =
        (rawSelect "document")?ready fn |> ignore
    
module Filmow =
    let startAsync = async {
        let movies = JQuery.rawSelect ".list_items_wrapper .movie_list_item"
        
        if movies?length > 0 then
            ".list_items_wrapper"
            |> JQuery.rawSelect
            |> fun x -> x?prepend """
                <div>
                    <a href="javascript:void(0);" onclick="window.randomList()">Random</a>
                    <br/>
                    "<span id="random_result"></span>"
                </div> """
                
            Core.setInterval 500 (fun _ ->
                ".btn-lists-infinite-scroll"
                |> JQuery.trySelect
                |> function
                    | None -> ()
                    | Some button ->
                        if button?attr "disabled" <> "disabled"
                        then button?click ()
            )
            
            Dom.window?randomList <- fun () ->
                let movies = JQuery.rawSelect ".list_items_wrapper .movie_list_item"
                
                ".btn-lists-infinite-scroll"
                |> JQuery.trySelect
                |> function
                    | None -> ()
                    | Some button ->
                        if button?attr "disabled" <> "disabled" then
                            "#random_result"
                            |> JQuery.rawSelect
                            |> JQuery.setHtml "Load all movies"
                        else
                            let random = Random().Next (0, movies?length)
                            
                            let movieName =
                                ".movie_list_item"
                                |> JQuery.rawSelect
                                |> fun x -> x?eq(random)?find("img")?attr("alt")
                            
                            "#random_result"
                            |> JQuery.rawSelect
                            |> JQuery.setHtml (sprintf "%d - %s" random movieName)
    

    }

module Program =
    printfn "UserScripts.Filmow loaded (Program.fs)"
    
    importAll "./index.scss"
    
    JQuery.ready (fun () ->
        Filmow.startAsync
        |> Async.StartImmediate
    )
    
