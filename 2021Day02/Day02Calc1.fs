module Day02Calc1

open System
open System.IO
open Common

let toCommand line =
    let cmd =
        line
        |> (splitOn " ")
        |> (fun x-> (x[0],int32 x[1]))
    match cmd with
    | ("up",y) -> (0,-y)
    | ("down",y) -> (0,y)
    | ("forward",x)-> (x,0)
    | _ -> failwith "unexpected"

let nextPos1 (x,y) (dx,dy) =
    x+dx,y+dy

let nextPos2 (x,y,aim) (dx,daim) =
//    printfn $"x:{x} y:{y} aim:{aim} cmdx:{dx} cmdaim:{daim}"
    x+dx, y+aim*dx, aim+daim    
(*
let nextPos (x,y,dir) (line:string) =
    let cmd = line.[0]
    let arg = int (line.Substring(1))
    match cmd with
    | 'E' -> moveDist E arg (x,y,dir)
    | 'S' -> moveDist S arg (x,y,dir)
    | 'W' -> moveDist W arg (x,y,dir)
    | 'N' -> moveDist N arg (x,y,dir)
    | 'F' -> moveDist dir arg (x,y,dir)
    | 'R'| 'L' -> (x,y, (newDir line dir))

    | _ -> failwith (sprintf "unexpected command %s" line)

let finalPos lines =
    lines
    |> Seq.fold nextPos (0,0,E)

*)
let Calc1 filename =
    let x,y =
        File.ReadLines filename
        |> Seq.map toCommand
        |> Seq.fold nextPos1 (0,0)
    x*y

let Calc2 filename =
    let x,y,_ =
        File.ReadLines filename
        |> Seq.map toCommand
        |> Seq.fold nextPos2 (0,0,0)
    x*y
