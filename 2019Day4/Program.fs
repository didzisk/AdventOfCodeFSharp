// Learn more about F# at http://fsharp.org

open System
open Permutations

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    let combinations = permutations [0;1;2;3;4] Set.empty
    printfn "%A" combinations

    [1;2;3]
    |> permToInt
    |> printfn "%A"

    123
    |> intToPerm
    |> printfn "%A"

    123
    |> intToPerm
    |> listIsGrowing
    |> printfn "%A"

    11223
    |> intToPerm
    |> listIsGrowing
    |> printfn "%A"

    13223
    |> intToPerm
    |> listIsGrowing
    |> printfn "%A"

    13223
    |> intToPerm
    |> listHasRepeat
    |> printfn "Expected HasRepeat=true %A"

    13423
    |> intToPerm
    |> listHasRepeat
    |> printfn "Expected HasRepeat=false %A"

    seq{ 
        for i = 240920 to 789857 do
            let L = intToPerm i
            if (listHasRepeat L) && (listIsGrowing L) then
                yield i
    }
    |> Seq.length
    |> printfn "Num elements: %A"

    13423
    |> intToPerm
    |> listHasIsolatedPair
    |> printfn "Expected HasIsolatedPair=false %A"
    11123
    |> intToPerm
    |> listHasIsolatedPair
    |> printfn "Expected HasIsolatedPair=false %A"

    11423
    |> intToPerm
    |> listHasIsolatedPair
    |> printfn "Expected HasIsolatedPair=true %A"

    12422
    |> intToPerm
    |> listHasIsolatedPair
    |> printfn "Expected HasIsolatedPair=true %A"

    seq{ 
        for i = 240920 to 789857 do
            let L = intToPerm i
            if (listHasIsolatedPair L) && (listIsGrowing L) then
                printfn "%i" i
                yield i
    }
    |> Seq.length
    |> printfn "Num elements: %A"

    0 // return an integer exit code
