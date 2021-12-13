module Day13Calc

open System.IO
open System
open Common

[<Struct>]
type Point = 
    { X: int
      Y: int}

type FoldCommand =
    | FoldX of int
    | FoldY of int

let parselines filename =
    let arr =
        File.ReadAllLines filename
        |> Seq.toArray
    let theEmpty =
        arr
        |> Array.findIndex (fun line -> line = "")

    let paper =
        arr
        |> Array.take theEmpty
        |> Array.map (fun line->
            let coords = split "," line
            let x = int coords[0]
            let y = int coords[1]
            {X=x;Y=y}
            )
        |> Set.ofArray

    let folds =
        arr
        |> Array.skip (theEmpty+1)
        |> Array.map (fun line->
            let cmd = split "=" line
            if cmd[0] = "fold along x" then
                FoldX (int cmd[1])
            else
                FoldY (int cmd[1])
            )

    paper, folds

let folder (paper:Set<Point>) cmd = 
    paper
    |> Set.map (fun pt ->
        match cmd with
        | FoldX foldx -> 
            {pt with 
                X =
                    if foldx > pt.X then 
                        pt.X
                    else
                        2 * foldx-pt.X
            }
        | FoldY foldy -> 
            {pt with
                Y =
                    if foldy > pt.Y then
                        pt.Y
                    else
                        2 * foldy - pt.Y
            }
        )

let Calc1 filename =
    let paper, folds =
        parselines filename

    folder paper folds[0] 
    |> Set.count

let showpaper (paper:Set<Point>) = 
    Console.Clear()
    paper
    |> Set.iter(fun pt->
        Console.CursorTop<-pt.Y
        Console.CursorLeft<-pt.X
        Console.Write("#")
        )
    Console.ReadLine() |> ignore
        
let Fold2 filename =
    let paper, folds = parselines filename

    Seq.fold folder paper folds
