// Learn more about F# at http://fsharp.org

open System
open Day15Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    "Day15InputExample.txt"
    |> Lines
    |> nextNum
    |> printfn "%d"

    "Day15InputExample.txt"
    |> Lines
    |> calc1
    |> printfn "Ex1 %d"

    "Day15Input.txt"
    |> Lines
    |> calc1
    |> printfn "Calc1 %d"

    "Day15InputExample.txt"
    |> Lines
    |> calc2
    |> printfn "Ex2 %d"


    0 // return an integer exit code
