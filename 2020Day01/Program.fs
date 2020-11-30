// Learn more about F# at http://fsharp.org

open System
open Day1Calc

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    let a=Lines "Day1Input.txt"
    a
    |> Seq.iter (fun x->
        printfn "%A" x
    )


    0 // return an integer exit code
