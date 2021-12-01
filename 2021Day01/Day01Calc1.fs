module Day01Calc1

open System
open System.IO

let Lines filename =
    File.ReadLines filename
    |> Seq.map int32
    |> Seq.toArray

let Increasing inpseq =
    inpseq
    |> Seq.pairwise
    |> Seq.map(fun (x,y)->y-x)
    |> Seq.filter(fun x-> x>0)
    |> Seq.length

let triplets inpseq =
    seq {
        let len = Array.length inpseq
        for i = 0 to len-3 do
            yield inpseq[i]+inpseq[i+1]+inpseq[i+2]
    }