module Day12Calc

open System
open System.IO
open Common

let Lines filename = 
    File.ReadLines filename
    |> Seq.toArray


