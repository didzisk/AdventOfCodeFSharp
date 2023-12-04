open System
open _2023Day03
open Utils.Downloader

[<Literal>]
let inputFile = __SOURCE_DIRECTORY__ + "\input.txt"

downloadInput __SOURCE_DIRECTORY__

let text = @"
467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598.."
let linesEx = text.Split([|'\n'|]);
let lines =
    System.IO.File.ReadLines inputFile
    |> Array.ofSeq
let private neighbors (row, col) =
    [(row-1, col-1); (row-1, col); (row-1, col+1)
     (row,   col-1);               (row,   col+1)
     (row+1, col-1); (row+1, col); (row+1, col+1);]

type ScanState = {Row:int; Col: int; CurrentInt: int; IsValid: bool; AllInts: int list}

let hasSymbolNeighbor row col =
    neighbors (row, col)
    |> List.map
        (fun (r,c) ->
            try
                let sym = lines[r][c]
                not (Char.IsDigit sym || sym = '.')
            with
                | _ -> false
        )
    |> List.exists id
    
let folder (state:ScanState) (sym:char) =
    let addInts=
        if Char.IsDigit sym then
            []
        else
            [state.CurrentInt]
    let currentInt = 
        if Char.IsDigit sym then
            state.CurrentInt * 10 + Int32.Parse(sym.ToString())
        else
            0
    let hasGoodNeighbors = hasSymbolNeighbor state.Row state.Col
    let isValid =
        if Char.IsDigit sym then
            state.IsValid || hasGoodNeighbors
        else
            false
    let newInts =
        if state.IsValid then
            state.AllInts @ addInts
        else
            state.AllInts
    {state with Col = state.Col+1; AllInts = newInts; CurrentInt = currentInt; IsValid = isValid }
    
let parseLine (row:int) (s:string)  =
    let lineState =
        ({Row = row; Col = 0; IsValid = false; CurrentInt = 0; AllInts = List.empty }, s+".")
        ||> Seq.fold folder
    lineState.AllInts
    |> Seq.sum
    
let Solve1 ()=
    lines
    |> Array.mapi parseLine
    |> Array.sum
    |> printfn "%A"
    
Part2.Solve2 ()
    