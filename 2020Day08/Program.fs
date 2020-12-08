// Learn more about F# at http://fsharp.org

open System
open Day8Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    Lines "Day8InputExample.txt"
    |> calc1
    |> printfn "%A"

    Lines "Day8Input.txt"
    |> calc1
    |> printfn "%A"

    Lines "Day8InputExample.txt"
    |> calc2 1
    |> printfn "%A"

    Lines "Day8Input.txt"
    |> calc2 1
    |> printfn "%A"

    0 // return an integer exit code
