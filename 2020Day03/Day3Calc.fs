module Day3Calc

open System.IO

let Lines filename = 
    File.ReadLines filename

let aLines filename = 
    File.ReadLines filename
    |> Seq.map int32
    |> Seq.toArray

let parseInput (lines:string seq) =
    lines |> Seq.toArray

(*    seq {
        for x=0 to (arr.[0].Trim()).Length-1 do
            for y=0 to arr.Length-1 do
                if arr.[y].[x] = '#' then
                    yield (x,y)
    }
    *)

let parseSize (lines:string seq) =
    let arr=lines |> Seq.toArray
    arr.Length (*rows*), arr.[0].Trim().Length (*cols*)

let rec downOneStep (grid:string[]) (rowsD:int) (colsR:int) currPos =
    let row, col, currCount = currPos
    let rows, cols = parseSize grid
    if row+rowsD>=rows then
        printfn "Result %A" currCount
        currCount
    else
        let nextRow, nextCol = 
            if (col+colsR >= cols) then
                row+rowsD, col+colsR-cols
            else
                row+rowsD, col+colsR

        if grid.[nextRow].[nextCol]='#' then
//            printfn "%A row:%A col:%A" currCount nextRow nextCol 
            downOneStep grid rowsD colsR (nextRow, nextCol, currCount+1)
        else
//            printfn "%A row:%A col:%A" currCount nextRow nextCol 
            downOneStep grid rowsD colsR (nextRow, nextCol, currCount)

let solve rows cols filename =
    let pInput =
        filename
        |> Lines
        |> parseInput
    downOneStep pInput rows cols (0, 0, 0)