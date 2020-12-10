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
    |> printfn "%d"

    calc1 "Day10Input.txt"
    |> printfn "%d"

    calc2 "Day10InputExample.txt"
    |> printfn "%d"
    calc2 "Day10InputExample2.txt"
    |> printfn "Ex2 %d"

    calc2 "Day10Input.txt"
    |> printfn "Part2 %d"

    0 // return an integer exit code
