module Day3Calc

open System.IO

let Lines filename = 
    File.ReadLines filename

let parseInput (lines:string seq) =
    lines |> Seq.toArray

let parseSize (lines:string seq) =
    let arr=lines |> Seq.toArray
    arr.Length (*rows*), arr.[0].Trim().Length (*cols*)

let rec downOneStep (grid:string[]) (rowsD:int) (colsR:int) currPos =
    let row, col, currCount = currPos
    let rows, cols = parseSize grid
    if row+rowsD>=rows then
        currCount
    else
        let nextRow, nextCol = 
            if (col+colsR >= cols) then
                row+rowsD, col+colsR-cols
            else
                row+rowsD, col+colsR

        if grid.[nextRow].[nextCol]='#' then
            downOneStep grid rowsD colsR (nextRow, nextCol, currCount+1)
        else
            downOneStep grid rowsD colsR (nextRow, nextCol, currCount)

let solve rows cols filename : int64 =
    let pInput =
        filename
        |> File.ReadLines
        |> Seq.toArray
    downOneStep pInput rows cols (0, 0, 0)
    |> int64