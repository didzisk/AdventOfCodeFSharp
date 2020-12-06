// Learn more about F# at http://fsharp.org

open System
open Day7Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    Lines "Day7InputExample.txt"
    |> Array.iter (printfn "%A")

    0 // return an integer exit code
