﻿// Learn more about F# at http://fsharp.org

open System
open Day17Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    "Day17InputExample.txt"
    |> Lines
    |> Array.iter (printfn "%s")











    0 // return an integer exit code
