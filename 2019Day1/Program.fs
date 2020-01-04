// Learn more about F# at http://fsharp.org

open System
open Day1Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    
    printfn "%A" (Day1Calc.Fuel1 12)
    printfn "%A" (Day1Calc.Fuel1 14)
    printfn "1969 expect 654 %A" (Day1Calc.Fuel1 1969)
    printfn "100756 expect 33583 %A" (Day1Calc.Fuel1 100756)

    let a=Lines "Day1Input.txt"
    a
    |> Seq.iter (fun x->
        printfn "%A" x
    )

    let b=Seq.sum a

    printfn "Part1weight %A" b

    "Day1Input.txt"
    |> Lines
    |> FuelTotal
    |> printfn "Part1fuel %A"

    printfn "1969 expect 966 %A" (Day1Calc.Fuel2 1969)
    printfn "100756 expect 50346 %A" (Day1Calc.Fuel2 100756)

    "Day1Input.txt"
    |> Lines
    |> FuelTotal2
    |> printfn "Part2fuel %A"

    0 // return an integer exit code
