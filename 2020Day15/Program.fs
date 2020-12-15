// Learn more about F# at http://fsharp.org

open System
open Day15Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    "Day15InputExample.txt"
    |> MLines
    |> printfn "%A"

    "Day15InputExample.txt"
    |> MLines
    |> calc1
    |> printfn "Ex1 %d"

    "Day15Input.txt"
    |> MLines
    |> calc1
    |> printfn "Calc1 %d"


    printfn "*****************"
    "Day15Input.txt"
    |> MLines
    |> calc2
    |> printfn "Calc2 %d"

    "Day15InputExample.txt"
    |> MLines
    |> calc2
    |> printfn "Ex2 %d"

    0 // return an integer exit code
