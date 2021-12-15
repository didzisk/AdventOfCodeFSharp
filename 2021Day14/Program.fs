open Day14Calc

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

//"Day14InputExample.txt"
//|> parseInput
//||> printfn "%A %A"

let (arr, startWorld) =
    "Day14Input.txt"
    |> parseInput

//nextWorld startWorld arr
//|> printfn "%A"

//Calc1 startWorld arr 10
//|> Seq.iter (printfn "%A")

startWorld
|> Day14Calc2.treeWorld
|> (printfn "%A")

startWorld
|> Day14Calc2.treeWorld
|> Day14Calc2.nextWorld arr 10
|> Map.iter (fun k v -> printfn $"{k}:{v}")


startWorld
|> Day14Calc2.treeWorld
|> Day14Calc2.nextWorld arr 10
|> Day14Calc2.calcResults
|> List.iter (fun (k,v) -> printfn $"{k}:{v}")

startWorld
|> Day14Calc2.treeWorld
|> Day14Calc2.nextWorld arr 10
|> Day14Calc2.calcResults
|> Day14Calc2.calcMinMax startWorld
|> printfn "%A"

startWorld
|> Day14Calc2.treeWorld
|> Day14Calc2.nextWorld arr 40
|> Day14Calc2.calcResults
|> Day14Calc2.calcMinMax startWorld
|> printfn "%A"