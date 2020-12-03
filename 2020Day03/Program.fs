// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main _ =

    Day3Calc.solve 1 3 "Day3InputExample.txt"
    |> printfn "Pt1 Example D1 R3: %d"

    Day3Calc.solve 1 3 "Day3Input.txt"
    |> printfn "Pt1 Solution D1 R3: %d"

    Day3Calc.solve 1 1 "Day3InputExample.txt"
    * Day3Calc.solve 1 3 "Day3InputExample.txt"
    * Day3Calc.solve 1 5 "Day3InputExample.txt"
    * Day3Calc.solve 1 7 "Day3InputExample.txt"
    * Day3Calc.solve 2 1 "Day3InputExample.txt"
    |> printfn "Pt2 Example: %d"

    printfn ""

    Day3Calc.solve 1 1 "Day3Input.txt"
    * Day3Calc.solve 1 3 "Day3Input.txt"
    * Day3Calc.solve 1 5 "Day3Input.txt"
    * Day3Calc.solve 1 7 "Day3Input.txt"
    * Day3Calc.solve 2 1 "Day3Input.txt"
    |> printfn "Pt2 Solution: %d"

    0 // return an integer exit code
