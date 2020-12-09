// Learn more about F# at http://fsharp.org

open System
open Day9Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    calc1 "Day9InputExample.txt" 5
    |> Array.iter (printfn "%d")
    
    calc1 "Day9Input.txt" 25
    |> Array.iter (printfn "%d")

    calc2 "Day9Input.txt" 375054920L
    |> printfn "%A"


    0 // return an integer exit code
