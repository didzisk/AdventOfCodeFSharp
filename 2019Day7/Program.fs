// Learn more about F# at http://fsharp.org

open System
open Permutations
open IntcodeComputer

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    permutations [0;1;2;3;4] Set.empty |> printfn "%A"

    let combinations = permutations [0;1;2;3;4] Set.empty

    let icode="3,8,1001,8,10,8,105,1,0,0,21,42,67,84,97,118,199,280,361,442,99999,3,9,101,4,9,9,102,5,9,9,101,2,9,9,1002,9,2,9,4,9,99,3,9,101,5,9,9,102,5,9,9,1001,9,5,9,102,3,9,9,1001,9,2,9,4,9,99,3,9,1001,9,5,9,1002,9,2,9,1001,9,5,9,4,9,99,3,9,1001,9,5,9,1002,9,3,9,4,9,99,3,9,102,4,9,9,101,4,9,9,102,2,9,9,101,3,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,99,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,99"

    let initAmp phase = //one amplifier
        Machine.runFromStringWithInputs icode [phase] (printfn "%A")

    let runAmp signal st= 
        Machine.run (printfn "%A") {st with Inputs=[signal]}

    let initAmps phases =
        phases |> List.map (fun x-> printf "%A " (int x)) |> ignore
        printfn ""
        phases
        |> List.map initAmp

    let calcOutput1 amps =
        let action signalSoFar st = 
            let newst=runAmp signalSoFar st
            newst.Result
        amps
        |> List.fold action 0L

    let calc1disp = 
        permutations [0L;1L;2L;3L;4L] Set.empty 
        |> Seq.map initAmps
        |> Seq.map calcOutput1
        |> Seq.iter  (printfn "%A") 

    let calc1 = 
        permutations [0L;1L;2L;3L;4L] Set.empty 
        |> Seq.map initAmps
        |> Seq.map calcOutput1
        |> Seq.max 

    calc1 |> printfn "max result %A"
    //combinations    |> Seq.map
    let calc2disp = 
        permutations [5L;6L;7L;8L;9L] Set.empty 
        |> Seq.map initAmps
        |> Seq.map calcOutput1
        |> Seq.iter  (printfn "%A") 
    calc2disp |> ignore

    let calcOutput2 inVal amps =
        let action (signalSoFar,r) st = 
            let newst=runAmp signalSoFar st
            newst.Result, newst.ReturnMode
        amps
        |> List.fold action inVal


    let rec calc2 inVal amps = //takes initialized amps, feeds with signal
        let (res, r) = calcOutput2 inVal amps
        if r=Machine.RanToHalt then
            res
        else
            res
    
    0 // return an integer exit code
