﻿open Day1Calc

[<EntryPoint>]
let main _ =
    printfn "Hello World from F#!"
    let a = aLines "Day1Input.txt"

    a
    |> Seq.iter (fun x->
        printfn "%A" x
    )

    printfn ""

    for i = 0 to a.Length-1 do
        for j = i to a.Length-1 do
            for k = j to a.Length-1 do
                if a.[i] + a.[j] + a.[k] = 2020 then
                    printfn "%A" (a.[i] * a.[j] * a.[k])

    0 // return an integer exit code
