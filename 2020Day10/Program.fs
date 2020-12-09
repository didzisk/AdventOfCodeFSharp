// Learn more about F# at http://fsharp.org

open System
open Day10Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    Lines "Day10InputExample.txt"
    |> printfn "%A"


    0 // return an integer exit code
