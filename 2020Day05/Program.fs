// Learn more about F# at http://fsharp.org

open System
open Day5Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    createSeat "FBFBBFFRLR" |> printfn "FBFBBFFRLR : %d"
    createSeat "BFFFBBFRRR" |> printfn "BFFFBBFRRR : %d"

    createSeat "FFFFFFFLLL" |> printfn "FFFFFFFLLL : %d"
    createSeat "BBBBBBBLLL" |> printfn "BBBBBBBLLL : %d"

    Lines "Day5input.txt"
    |> Array.map createSeat
    |> Array.max
    |> printfn "maxSeatId1 : %d"

    Lines "Day5input.txt"
    |> Array.map createSeat2
    |> Array.sort
    |> Array.mapi (fun i x -> (i+89), x)
    |> Array.filter (fun (x,y) -> x<y)
    |> Array.map (fun (x,y)->x)
    |> Array.min
    |> printfn "*************missing %d"

    Lines "Day5input.txt"
    |> Array.map createSeat2
    |> Array.max
    |> printfn "maxSeatId1 : %d"

    for i = 89 to 1023 do
        idToSeat i
        |> printfn "%s"
    Console.ReadLine ()
    0 // return an integer exit code
