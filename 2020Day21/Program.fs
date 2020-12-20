// Learn more about F# at http://fsharp.org

open System
open Day21Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    "Day21InputExample.txt"
    |> Lines
    |> printfn "%A"
    0 // return an integer exit code
