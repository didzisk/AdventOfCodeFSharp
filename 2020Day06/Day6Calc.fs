module Day6Calc

open System.IO
open Common

let Lines filename = 
    File.ReadLines filename
    |> Seq.toArray

let createSeat2 (line:string) =
    line.ToCharArray()
    |> Array.mapi (
        fun i x -> 
            let mask = 0b1000000000 >>> i
            if (x='B' || x='R') then
                mask
            else
                0
        )
    |> Array.sum

let bitmaskToChar id mask =
    if id &&& mask = mask then
        if mask>7 then
            'R'
        else
            'B'
    else
        if mask>7 then
            'L'
        else
            'F'

let idToSeat (id:int) =
    seq {
        for i= 0 to 9 do
            let mask = 0b1000000000 >>> i
            yield bitmaskToChar id mask
            }
    |> asString
