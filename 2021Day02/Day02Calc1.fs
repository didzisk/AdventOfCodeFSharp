module Day02Calc1

open System
open System.IO

let Lines filename =
    File.ReadLines filename
    |> Seq.map int32
    |> Seq.toArray

