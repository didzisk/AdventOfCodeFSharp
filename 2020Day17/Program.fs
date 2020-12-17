// Learn more about F# at http://fsharp.org

open System
open Day17Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    "Day17InputExample.txt"
    |> Lines
    |> Array.iter (printfn "%s")

    "Day17InputExample.txt"
    |> calc1
    |> printfn "Ex1: %d"

    "Day17Input.txt"
    |> calc1
    |> printfn "Part1: %d"

    "Day17InputExample.txt"
    |> Day17Calc2.calc2
    |> printfn "Ex2: %d"

    "Day17Input.txt"
    |> Day17Calc2.calc2
    |> printfn "Part2: %d"








    0 // return an integer exit code
