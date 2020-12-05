module Day5Calc

open System.IO
open Common

let Lines filename = 
    File.ReadLines filename
    |> Seq.toArray
(*
Start by considering the whole range, rows 0 through 127.
F means to take the lower half, keeping rows 0 through 63.
B means to take the upper half, keeping rows 32 through 63.

last 3 characters of FBFBBFFRLR:

Start by considering the whole range, columns 0 through 7.
R means to take the upper half, keeping columns 4 through 7.
L means to take the lower half, keeping columns 4 through 5.
The final R keeps the upper of the two, column 5.
So, decoding FBFBBFFRLR reveals that it is the seat at row 44, column 5.

Every seat also has a unique seat ID: multiply the row by 8, then add the column. In this example, the seat has ID 44 * 8 + 5 = 357.

Here are some other boarding passes:

BFFFBBFRRR: row 70, column 7, seat ID 567.
FFFBBBFRRR: row 14, column 7, seat ID 119.
BBFFBBFRLL: row 102, column 4, seat ID 820.
1000000000
*)
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



//brute force shame:
let createSeat (line:string) =
    let srow= line.Substring(0,7)
    let mutable k =
        if srow.[0]='B' then
            64
        else
            0
    if srow.[1]='B' then
        k<-k+32
    else
        ()
    if srow.[2]='B' then
        k<-k+16
    else
        ()
    if srow.[3]='B' then
        k<-k+8
    else
        ()
    if srow.[4]='B' then
        k<-k+4
    else
        ()
    if srow.[5]='B' then
        k<-k+2
    else
        ()
    if srow.[6]='B' then
        k<-k+1
    else
        ()
    let scol= line.Substring(7,3)
    let mutable c =
        if scol.[0]='R' then
            4
        else 
            0
    if scol.[1]='R' then
        c<-c+2
    else
        ()
    if scol.[2]='R' then
        c<-c+1
    else
        ()
    8*k+c

