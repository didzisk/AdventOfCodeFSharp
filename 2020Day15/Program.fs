// Learn more about F# at http://fsharp.org

open System
open Day15Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    "Day15InputExample.txt"
    |> Lines
    |> Array.iter (printfn "%s")

    0 // return an integer exit code
