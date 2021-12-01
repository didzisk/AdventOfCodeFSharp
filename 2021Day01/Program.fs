open Day01Calc1

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

"Day01InputExample.txt"
|> Day01Calc1.Lines
|> Seq.map int32
|> Seq.pairwise
|> printfn "%A"

"Day01InputExample.txt"
|> Day01Calc1.Lines
|> Seq.map int32
|> Seq.pairwise
|> Seq.map(fun (x,y)->y-x)
|> printfn "%A"

"Day01InputExample.txt"
|> Day01Calc1.Lines
|> Seq.map int32
|> Seq.pairwise
|> Seq.map(fun (x,y)->y-x)
|> Seq.filter(fun x-> x>0)
|> printfn "filtered %A"


"Day01Input.txt"
|> Day01Calc1.Lines
|> Increasing
|> printfn "filtered %A"

"Day01InputExample.txt"
|> Day01Calc1.Lines
|> triplets
|> Increasing
|> printfn "Part 2 ex %A"

"Day01Input.txt"
|> Day01Calc1.Lines
|> triplets
|> Increasing
|> printfn "Part 2 ex %A"
