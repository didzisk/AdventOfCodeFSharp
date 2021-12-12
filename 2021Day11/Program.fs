open Day11Calc

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

//("Day11Inputexample.txt"
//|> readInput
//|> showArray formatfun9
//,0)
//|> addOne
//|> flashAllNeighbors
//|> showResult
//|> addOne
//|> flashAllNeighbors
//|> showResult
//|> ignore

//("Day11Input.txt"
//|> readInput
//|> showArray formatfun9
//,0, 0)
//|> Calc1
//|> ignore

("Day11Input.txt"
|> readInput
|> showArray formatfun9
,0, 0)
|> Calc2
|> ignore
