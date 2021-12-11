module Day04Calc1

open System
open System.IO
open Common

let Lines filename =
    File.ReadLines filename


let isEmptyRow arr =
    arr
    |> Array.forall (fun x->x="")

let hasEmptyRow arr =
    arr
    |> Array.exists isEmptyRow

let hasEmptyCol arr =
    arr
    |> Array.transpose
    |> hasEmptyRow

let arrSum (arr:string[][]) =
    arr
    |> Array.map (
        fun x-> 
            x
            |> Array.map (fun x-> 
                if x="" then
                    0
                else
                    int32 x)
            |> Array.sum
        )
    |> Array.sum

let Calc1 filename =
    let all = Lines filename
    let commands = 
        all
        |> Seq.head
        |> splitOn(",")


    let arraysText = 
        all 
        |> Seq.skip 2
        |> Seq.toArray

    let numArrays = (arraysText.Length+1) / 6
    let allArrays = 
        [|
            for i = 0 to numArrays-1 do
                yield
                    [| for row = 0 to 4 do
                        yield arraysText[i*6+row]
                            |> splitOn (" ")
                    |]
        |]

    let nextState (allArrays:string[][][]) x =
        [|
            let numArrays = allArrays.Length
            for i = 0 to numArrays-1 do
                if (hasEmptyRow allArrays.[i]) || (hasEmptyCol allArrays.[i]) then
                    ()
                else
                    yield
                        [| for row = 0 to 4 do
                            yield
                                [|
                                    for col = 0 to 4 do
                                        if allArrays.[i].[row].[col] = x then
                                            yield ""
                                        else
                                            yield allArrays.[i].[row].[col]
                                |]
                        |]
        |]

    let nextStateWithLog (allArrays:string[][][]) currCall =

        printfn $"Current call: {currCall}"

        let all = nextState allArrays currCall

        all
        |> Array.iter (fun x->
            Array.iter (printfn "%A") x 
            printfn ""
            )

        let oneArray = 
            (
            all
            |> Array.filter(fun arr-> (hasEmptyRow arr) || (hasEmptyCol arr))
            )

        let cmd = 
            if oneArray.Length>0 then
                oneArray
                |> Array.iter (printfn "%A")
                printfn $"Solution {(arrSum oneArray.[0]) * (int32 currCall)} = {(arrSum oneArray.[0])} * {(int32 currCall)}"
            else 
                ()
        cmd
        
        Console.ReadLine() |> ignore

        all


    allArrays
    |> Array.iter (fun x->
        Array.iter (printfn "%A") x 
        printfn ""
        )

    commands
    |> Seq.fold nextStateWithLog allArrays
    |> ignore
    

    (*
    commands
    |> Seq.iter (fun x ->
        for i = 0 to numArrays-1 do
            for row = 0 to 4 do
                for col = 0 to 4 do
                    allArrays[i].[row].[col] <-
                        if allArrays[i].[row].[col] = x then
                            ""
                        else
                            allArrays[i].[row].[col]

        //allArrays
        //|> Array.iter (fun x->
        //    Array.iter (printfn "%A") x 
        //    printfn ""
        //    )
        let oneArray = 
            (
            allArrays
            |> Array.filter(fun arr-> (hasEmptyRow arr) || (hasEmptyCol arr))
            )

        let cmd = 
            if oneArray.Length>0 then
                oneArray
                |> Array.iter (printfn "%A")
                printfn $"Solution {(arrSum oneArray.[0]) * (int32 x)}"
                failwith "done"
            else 
                ()
        cmd

        //printfn "##########################################"

        //Console.ReadLine() |> ignore
        )
    *)

