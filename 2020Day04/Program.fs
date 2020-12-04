// Learn more about F# at http://fsharp.org

open System
open Day4Calc

[<EntryPoint>]
let main _ =
    printfn "Hello World from F#!"
    Lines "Day4inputExample.txt"
    |> passports
    |> Seq.toArray
    |> printfn "%A"

    Lines "Day4inputExample.txt"
    |> passports
    |> Seq.map createPassport
    |> printfn "%A"

    Lines "Day4input.txt"
    |> passports
    |> Seq.map createPassport
    |> Seq.filter passportValid
    |> Seq.length
    |> printfn "%A"

    Lines "Day4inputExampleValids.txt"
    |> passports
    |> Seq.map createPassport
    |> Seq.filter passportValid2
    |> Seq.length
    |> printfn "%A"

    Lines "Day4inputExampleInvalids.txt"
    |> passports
    |> Seq.map createPassport
    |> Seq.filter passportValid2
    |> Seq.length
    |> printfn "%A"

    Lines "Day4input.txt"
    |> passports
    |> Seq.map createPassport
    |> Seq.filter passportValid2
    |> Seq.length
    |> printfn "Part 2 result: %A"

    0 // return an integer exit code
