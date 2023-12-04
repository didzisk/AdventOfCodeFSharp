module _2023Day03.Part2

open System

[<Literal>]
let inputFile = __SOURCE_DIRECTORY__ + "\input.txt"

let lines =
    System.IO.File.ReadLines inputFile
    |> Array.ofSeq
let private neighbors (row, col) =
    [(row-1, col-1); (row-1, col); (row-1, col+1)
     (row,   col-1);               (row,   col+1)
     (row+1, col-1); (row+1, col); (row+1, col+1);]

type ScanState = {Row:int; Col: int; CurrentInt: int; Gear: (int * int) option; AllInts: (int * (int * int)) list}

let gearNeighbor row col =
    try
        neighbors (row, col)
        |> List.map
            (fun (r,c) ->
                try
                    if lines[r][c] = '*' then
                        Some (r,c)
                    else
                        None
                with
                    | _ -> None
            )
        |> List.find (fun x-> x.IsSome)
    with
        | _ -> None
    
let folder (state:ScanState) (sym:char) =
    let addInts=
        if Char.IsDigit sym then
            None
        else
            Some state.CurrentInt
    let currentInt = 
        if Char.IsDigit sym then
            state.CurrentInt * 10 + Int32.Parse(sym.ToString())
        else
            0
    let gearHere = gearNeighbor state.Row state.Col
    let gear =
        if Char.IsDigit sym then
            if state.Gear.IsSome then
                state.Gear
            else
                gearHere
        else
            None
    let newInts =
        match state.Gear with
        | Some g ->
            match addInts with
            | Some i -> state.AllInts @ [(i,g)]
            | _ -> state.AllInts
        | _ -> state.AllInts
    {state with Col = state.Col+1; AllInts = newInts; CurrentInt = currentInt; Gear = gear }
    
let parseLine (row:int) (s:string)  =
    let lineState =
        ({Row = row; Col = 0; Gear = None ; CurrentInt = 0; AllInts = List.empty }, s+".")
        ||> Seq.fold folder
    lineState.AllInts
    |> Array.ofList
    
let Solve2 () =
    let res =
        lines
        |> Array.mapi parseLine
        |> Array.collect id
        |> Array.sortBy(fun (_,(r,c))-> r*1000+c)
    res |> Array.iter (printfn "%A")
    printfn "--------------------------------"
    
    let muls =
        let mutable i = 0
        seq{
            while i+1 < res.Length do
                if snd res[i] = snd res[i+1] then
                    yield fst res[i] * fst res[i+1]
                    i <- i+2
                else
                    i<- i+1
        }
        
        |> Seq.toArray
    muls |> Array.iter (printfn "%A")
    printfn "--------------------------------"
    
    muls
    |> Array.sum
    |> (printfn "%A") 
