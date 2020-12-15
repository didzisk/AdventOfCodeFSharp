// Learn more about F# at http://fsharp.org

open System
open Day14Calc
open Common

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    "Day14InputExample.txt"
    |> Lines
    |> (printfn "%A")

    "Day14InputExample.txt"
    |> show1
    |> Seq.iter (printfn "%s")

    "Day14InputExample.txt"
    |> calc1
    |> (printfn "%A")

    "Day14Input.txt"
    |> calc1
    |> (printfn "%A")

    allmasks "42" "X1101X" 
    |> Seq.toArray
    |> (printfn "%A")

    allmasksInt "42" "X1101X"
    |> Seq.toArray
    |> (printfn "%A")


    "Day14InputExample2.txt"
    |> calc2
    |> (printfn "%A")

    "Day14InputExample2.txt"
    |> show2

    "Day14Input.txt"
    |> calc2
    |> (printfn "%A")


//    |> Array.map asBinary
//    |> (printfn "42:%s with all masks : %A" (asBinary 42L))

    0 // return an integer exit code
