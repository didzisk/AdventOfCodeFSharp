// Learn more about F# at http://fsharp.org

open System
open Day13Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    "Day13InputExample.txt"
    |> Lines
    |> Seq.iter (printfn "%s")

    "Day13InputExample.txt"
    |> Lines
    |> calc1
    |> (printfn  "%d")

    "Day13Input.txt"
    |> Lines
    |> calc1
    |> (printfn  "%d")

    //[ [(2,3);(3,5);(2,7)];//23 
    //  [(10,11); (4,22); (9,19)];
    //  [(10,11); (4,12); (12,13)];
    //  [(0,17); (2, 13); (3, 19)];
    //  [(3,17); (1, 13); (0, 19)];//3417 or 782?
    //  [(3,67);(2,7);(1,59);(0,61)]]//754022-4


    //|> Chinese

    CD [2;3;2] [3;5;7] //23
    |> printfn "%A"

    CD [0;3;2] [3;5;7] //93
    |> printfn "%A"

    CD [0;2;3] [17;13;19] //3417 or 782
    |> printfn "%A"

    CD [3;1;0] [17;13;19] //3417 = 3420-3
    |> printfn "%A"

    "Day13InputExample.txt"
    |> Lines
    |> calc2

    "Day13Input.txt"
    |> Lines
    |> calc2




    0 // return an integer exit code
