// Learn more about F# at http://fsharp.org

open System
open Day11Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    Lines "Day11InputExample.txt"
    |> Array.iter (printfn "%s")

    printfn ""

    Lines "Day11InputExample.txt"
    |> nextRound
    |> Array.iter (printfn "%s")

    printfn ""

    Lines "Day11InputExample.txt"
    |> nextRound
    |> nextRound
    |> Array.iter (printfn "%s")

    printfn ""

    Lines "Day11InputExample.txt"
    |> goNext
    |> Array.iter (printfn "%s")

    Lines "Day11InputExample.txt"
    |> goNext
    |> countPassengers
    |> printfn "Ex1: %d"


    Lines "Day11Input.txt"
    |> goNext
    |> countPassengers
    |> printfn "Pt1: %d"

    printfn "************** part2"


    Lines "Day11InputExample.txt"
    |> Array.iter (printfn "%s")

    printfn ""

    Lines "Day11InputExample.txt"
    |> nextRound2
    |> Array.iter (printfn "%s")

    printfn ""

    Lines "Day11InputExample.txt"
    |> nextRound2
    |> nextRound2
    |> Array.iter (printfn "%s")

    printfn ""

    Lines "Day11InputExample.txt"
    |> nextRound2
    |> nextRound2
    |> nextRound2
    |> Array.iter (printfn "%s")

    printfn ""

    Lines "Day11InputExample.txt"
    |> goNext2
    |> countPassengers
    |> printfn "Ex2: %d"

    Lines "Day11Input.txt"
    |> goNext2
    |> countPassengers
    |> printfn "Pt2: %d"


    0 // return an integer exit code
