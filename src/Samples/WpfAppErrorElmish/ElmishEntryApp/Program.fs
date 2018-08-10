// Learn more about F# at http://fsharp.org

open System
open Elmish.WPF
open Elmish

type Model =
    { Answer: string; CanSubmit: bool }

type Msg =
    | AnswerMsg of string
    | BadAnswerMsg of string
    | Submit 

let init() = { Answer = "hello"; CanSubmit = false}
    
let update (msg:Msg) (model:Model) =
    match msg with
    | AnswerMsg t -> { model with Answer = t; CanSubmit = true}
    | BadAnswerMsg t -> { model with CanSubmit = false}
    | Submit  -> model
let toValid (txt:string) =
    if txt.Contains("r") then
        Result.Error ("Bad R", BadAnswerMsg txt)
    else 
        Result.Ok (AnswerMsg txt)
let view _ _ = 
    [ 
        //"Increment" |> Binding.cmd (fun m -> Increment)
        "Submit" |> Binding.cmdIf (fun (m) -> (fun (e) -> Submit)) (fun (m) -> m.CanSubmit = true)
        //"Count" |> Binding.oneWay (fun m -> m.Count)
        "Answer" |> Binding.twoWayValidation (fun m -> (m.Answer)) (fun v m -> v |> toValid)
        //"Clock" |> Binding.vm (fun m -> m.Clock) clockViewBinding ClockMsg 
        ]

[<EntryPoint;STAThread>]
let main argv =
   Program.mkSimple init update view
    //|> Program.withSubscription subscribe
    |> Program.runWindow (new WpfAppErrorElmish.MainWindow())
