// Learn more about F# at http://fsharp.org

open System
open Day12Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    //"Day12Input.txt"
    //|> Lines
    //|> Seq.iter (printfn "%s")

    "Day12InputExample.txt"
    |> Lines
    |> finalPos
    |> printfn "example1 %A"
    "Day12InputExample.txt"
    |> Lines
    |> finalPos
    |> mDistance
    |> printfn "example1 %A"

    "Day12Input.txt"
    |> Lines
    |> finalPos
    |> mDistance
    |> printfn "Part1 %A"

    "Day12InputExample.txt"
    |> Lines
    |> finalPos2
    |> printfn "example2 %A"

    "Day12InputExample.txt"
    |> Lines
    |> finalPos2
    |> mDistance2
    |> printfn "example2 %A"

    "Day12Input.txt"
    |> Lines
    |> finalPos2
    |> mDistance2
    |> printfn "Part2 %A"


    0 // return an integer exit code
