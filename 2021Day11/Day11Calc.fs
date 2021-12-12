module Day11Calc
open System.IO
open System

let showArray formatfun all =
    Console.CursorTop <- 0
    Console.CursorLeft <- 0
    printfn ""
    all
    |> Array.iter (fun line->
            line
            |> Array.iter (fun x-> 
                formatfun x
                //let y = 
                //    if x>9 then
                //        "X"
                //    else string x
                let y = x
                printf "%A" y
                )  
            printfn ""
        )
    all

let formatfun9 x =
    let cmd = 
        if x=0 then
            Console.BackgroundColor <- ConsoleColor.Yellow
        else
            Console.BackgroundColor <- ConsoleColor.Black
    cmd

let showResult  (arr:int32[][], numFlashes:int) =
    showArray formatfun9 arr |> ignore
    printfn $"Number of flashes {numFlashes}"
    (arr,numFlashes) 

let readInput filename =
    File.ReadLines filename
    |> Seq.map (fun line->
        line
        |> Seq.toArray
        |> Array.map (fun x-> (int32 x)-48)
        )
    |> Seq.toArray

let neighbors =
    [|
        (-1,-1)
        (-1, 0)
        (-1, 1)

        ( 0,-1)
        ( 0, 1)

        ( 1,-1)
        ( 1, 0)
        ( 1, 1)
    |]

let addOne (arr:int32[][], numFlashes:int) =
    arr
    |> Array.map (fun line ->
            line
            |> Array.map (fun x->
                    //if x+1>10 then
                    //    0
                    //else
                        x+1
                )
        )
    , numFlashes

let rec flashAllNeighbors (arr:int32[][], oldNumFlashes:int) =
    let newArr=arr |> Array.copy
    let mutable numFlashes = 0
    let numRows = arr.Length-1
    for row = 0 to numRows do
        let numCols = arr.[row].Length - 1 
        for col = 0 to numCols do
            let doflash =
                if arr.[row].[col] > 9 then
                    numFlashes <- numFlashes + 1
                    newArr.[row].[col] <- 0
                    neighbors
                    |> Seq.iter (fun (dr, dc)->
                            let cmd = 
                                if row+dr<0 
                                    || row+dr>numRows 
                                    || col+dc<0 
                                    || col+dc>numCols
                                    || newArr.[row+dr].[col+dc] = 0 then
                                    ()
                                else
                                    newArr.[row+dr].[col+dc] <- newArr.[row+dr].[col+dc] + 1
                            cmd
                        )
                else
                    ()
            doflash
    //printfn $"Num flashes: {numFlashes}"
    //newArr |> showArray formatfun9 |> ignore
    if numFlashes = 0 then
        newArr, oldNumFlashes
    else 
        flashAllNeighbors (newArr, oldNumFlashes+numFlashes)

let sumState (arr:int32[][]) =
    arr
    |> Array.map (fun line->
            line
            |> Array.sum
        )
    |> Array.sum

let rec Calc1 (arr:int32[][], oldNumFlashes:int, numIterations) =
    let (newArr, numFlashes) =
        (arr, oldNumFlashes)
        |> addOne
        |> flashAllNeighbors
        |> showResult

    let ss = newArr |> sumState

    printfn $"Sum {ss}"

    if numIterations = 99 then
        (newArr, numFlashes)
    else
        Calc1 (newArr, numFlashes, numIterations+1)

let rec Calc2 (arr:int32[][], oldNumFlashes:int, numIterations) =
    let (newArr, numFlashes) =
        (arr, oldNumFlashes)
        |> addOne
        |> flashAllNeighbors
        |> showResult

    let ss = newArr |> sumState

    printfn $"Sum {ss} NumIterations {numIterations+1}"

    if ss = 0 then
        (newArr, numFlashes)
    else
        Calc2 (newArr, numFlashes, numIterations+1)



