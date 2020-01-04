module Day1Calc

open System.IO

let Fuel1 (x:int) =
    x / 3 - 2

let Lines filename = 
    File.ReadLines filename
    |> Seq.map int32


let FuelTotal (lines:seq<int32>) =
    lines
    |> Seq.map Fuel1
    |> Seq.sum 

let rec Fuel2 (x:int) =
    match Fuel1 x with
    | a when a<=0 -> 0
    | b -> b + Fuel2 b

let FuelTotal2 (lines:seq<int32>) =
    lines
    |> Seq.map Fuel2
    |> Seq.sum 
