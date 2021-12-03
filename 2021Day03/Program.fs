open Day03Calc1

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

//"Day03Input.txt"
//|> Calc1
//|> printfn "1 res %A"

"Day03InputExample.txt"
|> Calc1
|> printfn "1 ex %A"

//"Day03Input.txt"
//|> Calc2
//|> printfn "2 res %A"

"Day03InputExample.txt"
|> CalcOxygen
|> printfn "Oxygen ex %A"

"Day03InputExample.txt"
|> CalcCO2
|> printfn "CO2 ex %A"


let oxygen =
    "Day03Input.txt"
    |> CalcOxygen
    |> List.head
oxygen 
|> printfn "Oxygen %A"

let Co2 = 
    "Day03Input.txt"
    |> CalcCO2
    |> List.head
Co2 
|> printfn "CO2 %A"

printfn $"Result {oxygen * Co2}"