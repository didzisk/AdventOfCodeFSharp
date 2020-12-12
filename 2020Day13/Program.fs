// Learn more about F# at http://fsharp.org

open System
open Day13Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    "Day13InputExample.txt"
    |> Lines
    |> Seq.iter (printfn "%s")









    0 // return an integer exit code
