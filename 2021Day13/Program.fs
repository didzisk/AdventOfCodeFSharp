open Day13Calc

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

parselines "Day13InputExample.txt"
|> printfn "%A"


Calc1 "Day13Input.txt"
|> printfn "%A"

Fold2 "Day13InputExample.txt"
|> showpaper


Fold2 "Day13Input.txt"
|> showpaper

