// Learn more about F# at http://fsharp.org

open System
open Day8Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    Lines "Day8InputExample.txt"
    |> printfn "%A"


    0 // return an integer exit code
