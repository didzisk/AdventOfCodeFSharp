

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

open Day02Calc1

"Day02InputExample.txt"
|> Calc1
|> printfn "1 ex %A"

"Day02Input.txt"
|> Calc1
|> printfn "1 result %A"

"Day02InputExample.txt"
|> Calc2
|> printfn "2 ex %A"

"Day02Input.txt"
|> Calc2
|> printfn "2 result %A"
