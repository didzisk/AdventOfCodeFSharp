module Day15Calc

open System
open System.IO
open Common

let MLines filename =
    File.ReadLines filename
    |> Seq.head
    |> split ","
    |> Array.mapi (fun i x -> (int64 x, int64 i))
    |> Map.ofArray

let addLastPos (oldmap:Map<int64, int64>) (newVal:int64) (newPos:int64) =
    oldmap.Add(newVal, newPos)

//let getLastPos




let listUnfolder len (posmap:Map<int64, int64>, newVal:int64, pos:int64) =
    if pos = len then
        None
    else
        let nextval = 
            if posmap.ContainsKey(newVal) then
                pos - posmap.[newVal]
            else
                0L
        let nextMap = posmap.Add(newVal, pos)
        Some (nextval, (nextMap, nextval, pos+1L))

let numlist len (posmap:Map<int64, int64>) =
    //let mapStudents = students 
    //|> Array.map (fun s -> (s.getId(), s.getScore()))
    //|> Map.ofArray
    let pos = int64 (Map.count posmap)

    let nextval = 0L
        //if posmap.ContainsKey(newVal) then
        //    pos - posmap.[newVal]
        //else
        //    0L
    List.unfold (listUnfolder len) (posmap,  nextval, pos)

let calc1 list =
    let len = 2019L //- int64 (Map.count list)

    let reslist = numlist len list 
    match reslist with
    | [] -> failwith "unexpected"
    | ls-> List.last ls

let calc2 list =
    let reslist = numlist 29999999L list 
                          
    match reslist with
    | [] -> failwith "unexpected"
    | ls-> List.last ls