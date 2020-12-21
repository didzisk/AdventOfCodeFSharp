// Learn more about F# at http://fsharp.org

open System
open Day20Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    //"Day20Input.txt"
    //|> readTiles
    //|> printfn "%A"

    "Day20Input.txt"
    |> calc1


    "Day20Input.txt"
    |> calc2count

    "Day20Input.txt"
    |> calcFree2
    |> printfn "Answer 2: %d"





    0 // return an integer exit code
