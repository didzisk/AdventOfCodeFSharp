module Day17Calc2

open System
open System.IO
open Common

let Lines filename =
    File.ReadLines filename
    |> Seq.toArray

    //If a cube is active and exactly 2 or 3 of its neighbors are also active, 
    //the cube remains active. 
    //Otherwise, the cube becomes inactive.
    //If a cube is inactive but exactly 3 of its neighbors are active, 
    //the cube becomes active. 
    //Otherwise, the cube remains inactive.

type World = Set<(int*int*int*int)>

let initWorld (filename:string) : World =
    let arr = Lines filename
    let lenr = arr |> Array.length
    let lenc = arr.[0] |> String.length

    let pairs =
        seq {
            for row=0 to lenr-1 do
                for col = 0 to lenc-1 do
                    if arr.[row].[col] = '#' then
                        yield ((col,row,0,0))
                    else
                        ()
        }
    pairs |> Set.ofSeq

let numNeighbors xx yy zz ww (world:World) =
    seq {
        for x=xx-1 to xx+1 do
            for y=yy-1 to yy+1 do
                for z=zz-1 to zz+1 do
                    for w=ww-1 to ww+1 do 
                        if (x<>xx || y <>yy || z<>zz || w<>ww) && (world |> Set.contains (x,y,z,w)) then
                            yield 1
                        else
                            ()
    }
    |> Seq.sum

//1st gen: -1..len
//2nd gen: -2..len+1

//nth gen: -n..len+n-1

//If a cube is active and exactly 2 or 3 of its neighbors are also active, 
//the cube remains active. 
//Otherwise, the cube becomes inactive.
//If a cube is inactive but exactly 3 of its neighbors are active, 
//the cube becomes active. 
//Otherwise, the cube remains inactive.

let nextGen (len:int) (gen:int) (world:World) =
    seq{
            for w = -gen to len+gen-1 do
                for z= -gen to len+gen-1 do
                //printfn ""
                //printfn ""
                for y= -gen to len+gen-1 do
                    //printfn ""
                    for x= -gen to len+gen-1 do
                        //if world |> Set.contains (x,y,z) then
                        //    printf "#"
                        //else
                        //    printf "."
                        let n=numNeighbors x y z w world
                        if world |> Set.contains (x,y,z,w) then
                            match n with
                            | 2 | 3 -> yield (x,y,z,w)
                            | _ -> ()
                        else
                            if n=3 then
                                yield (x,y,z,w)
    }
    |> Set.ofSeq

let calc2 filename = 
    let len = Lines filename |> Array.length
    let finalState=
        initWorld filename
        |> (nextGen len 1)
        |> (nextGen len 2)
        |> (nextGen len 3)
        |> (nextGen len 4)
        |> (nextGen len 5)
        |> (nextGen len 6)
    finalState |> Set.count