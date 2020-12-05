// Learn more about F# at http://fsharp.org

open System
open Day6Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    for i = 89 to 1023 do
        idToSeat i
        |> printfn "%s"

    Lines "Day6Input.txt"
    |> Array.iter (printfn "%s")


    0 // return an integer exit code
