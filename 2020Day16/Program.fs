// Learn more about F# at http://fsharp.org

open System
open Day16Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    "Day16InputExample.txt"
    |> Lines
    |> Seq.iter (printfn "%s")

    "Day16InputExample.txt"
    |> readFields
    |> Seq.iter (printfn "%A")

    "Day16InputExample.txt"
    |> show1

    "Day16Input.txt"
    |> calc1
    |> printfn "Ex1 %d"

    "Day16InputExample2.txt"
    |> show2

    "Day16InputExample2.txt"
    |> show21

    "Day16Input.txt"
    |> show21
    

    calc2

    "Day16Input.txt"
    |> show22

    "Day16Input.txt"
    |> calc22
    |> printfn "Answer part 2: %d"

    0 // return an integer exit code
