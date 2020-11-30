module Day1Calc

open System.IO

let Lines filename = 
    File.ReadLines filename
    |> Seq.map int32



