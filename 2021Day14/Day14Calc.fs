module Day14Calc

open System
open System.IO
open Common
open System.Text

type Command =
    {
        Pair: char * char
        InChar:char
    }

let parseInput filename =
    let arr = 
        File.ReadLines filename
        |> Seq.cache
    let originalWorld = arr |> Seq.head
    
    arr 
    |> Seq.skip 2
    |> Seq.map(fun x-> 
            {Pair=(x[0],x[1]); InChar=x[6]}
        )
    , originalWorld

let nextWorld (oldWorld:string) (commands:seq<Command>) =
    let b = new StringBuilder()
    oldWorld
    |> Seq.pairwise
    |> Seq.map (fun (x,y)-> 
        let command = commands |> Seq.tryFind (fun c-> c.Pair = (x,y))
        match command with
        | Some z -> [|x;z.InChar|]
        | None -> [|x|]
        )
    |> Seq.iter (fun str -> b.Append str |> ignore)
    b.Append(oldWorld |> Seq.last).ToString()

let rec produceFinalWorld (oldWorld:string) (commands:seq<Command>) counter =
    let newWorld = nextWorld oldWorld commands
    if counter < 2 then
        newWorld
    else
        produceFinalWorld newWorld commands (counter-1)

let Calc1 (oldWorld:string) (commands:seq<Command>) counter =
    let str = produceFinalWorld oldWorld commands counter 

    str
    |> Seq.groupBy (fun x->x)
    |> Seq.map (fun (x,s)-> x, (Seq.length s))

