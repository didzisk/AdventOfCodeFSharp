// Learn more about F# at http://fsharp.org

open System
open Day24Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    "Day24InputExample1.txt"
    |> calc1
    |> printfn "(0,0): %d"

    "Day24InputExample.txt"
    |> calc1
    |> printfn "Ex1: %d"

    "Day24Input.txt"
    |> calc1
    |> printfn "Pt1: %d"






    0 // return an integer exit code
