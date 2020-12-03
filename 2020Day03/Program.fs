// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main argv =
    printfn "Right 3 down 1"
    Day3Calc.solve 1 3 "Day3InputExample.txt"
    |> ignore
    Day3Calc.solve 1 3 "Day3Input.txt"
    |> ignore

    Day3Calc.solve 1 1 "Day3InputExample.txt"
    |> ignore
    Day3Calc.solve 1 3 "Day3InputExample.txt"
    |> ignore
    Day3Calc.solve 1 5 "Day3InputExample.txt"
    |> ignore

    printfn ""

    Day3Calc.solve 1 1 "Day3Input.txt"
    |> ignore
    Day3Calc.solve 1 3 "Day3Input.txt"
    |> ignore
    Day3Calc.solve 1 5 "Day3Input.txt"
    |> ignore
    Day3Calc.solve 1 7 "Day3Input.txt"
    |> ignore
    Day3Calc.solve 2 1 "Day3Input.txt"
    |> ignore
    0 // return an integer exit code
