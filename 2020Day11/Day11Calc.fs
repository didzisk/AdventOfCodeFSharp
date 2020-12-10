module Day11Calc

open System.IO

let Lines filename = 
    File.ReadLines filename
    |> Seq.map int
    |> Seq.toArray

