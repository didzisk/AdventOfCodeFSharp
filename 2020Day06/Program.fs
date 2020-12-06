// Learn more about F# at http://fsharp.org

open System
open Day6Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    part1 "Day6InputExample.txt"
    |> printfn "%d"

    part1 "Day6Input.txt"
    |> printfn "%d"

    part2 "Day6InputExample.txt"
    part2 "Day6Input.txt"


    0 // return an integer exit code
