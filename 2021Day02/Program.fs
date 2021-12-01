

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

open Day02Calc1

"Day02Input.txt"
|> Lines
|> printfn "filtered %A"
