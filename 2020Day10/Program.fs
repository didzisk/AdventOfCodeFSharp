// Learn more about F# at http://fsharp.org

open System
open Day10Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    Lines "Day10InputExample.txt"
    |> hops
    |> printfn "%A"

    calc1 "Day10InputExample.txt"
    |> printfn "%A"

    calc1 "Day10Input.txt"
    |> printfn "%A"

    calc2 "Day10InputExample.txt"
    |> printfn "%A"
    calc2 "Day10InputExample2.txt"
    |> printfn "Ex2 %A"

    calc2 "Day10Input.txt"
    |> printfn "Part2 %A"

    0 // return an integer exit code
