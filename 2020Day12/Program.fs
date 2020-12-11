// Learn more about F# at http://fsharp.org

open System
open Day12Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    "Day12Input.txt"
    |> Lines
    |> Array.iter (printfn "%s")

    0 // return an integer exit code
