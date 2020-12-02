module Day3Calc

open System.IO

let Lines filename = 
    File.ReadLines filename

let aLines filename = 
    File.ReadLines filename
    |> Seq.map int32
    |> Seq.toArray

