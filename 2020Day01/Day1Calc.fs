module Day1Calc

open System.IO

let Lines filename = 
    File.ReadLines filename
    |> Seq.map int32

let aLines filename = 
    File.ReadLines filename
    |> Seq.map int32
    |> Seq.toArray



