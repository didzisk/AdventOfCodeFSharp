module Day10Calc

open System.IO
open Common
open System.Text.RegularExpressions

let Lines filename = 
    File.ReadLines filename
    |> Seq.toArray

