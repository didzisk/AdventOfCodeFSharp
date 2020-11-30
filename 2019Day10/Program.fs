// Learn more about F# at http://fsharp.org

open System
open GridForm

[<EntryPoint>]
let main argv =
    DictCalc.MainCalc |> ignore
    Console.ReadLine()
    0    
