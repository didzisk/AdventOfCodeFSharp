module Day15Calc

open System
open System.IO
open Common

let Lines filename =
    let arr = 
        File.ReadLines filename
        |> Seq.head
        |> split ","
    Array.Reverse arr
    arr
    |> Array.toList
    |> List.map int64

let addLastPos (oldmap:Map<int64, int64>) (newVal:int64) (newPos:int64) =
    oldmap.Add(newVal, newPos)

//let getLastPos


let nextNum list =
    match list with 
    | x::xs ->
        let agearr =
            list
            |> List.mapi (fun i elm -> i,elm)
            |> List.filter (fun (i,elm) -> elm =x)
            |> List.toArray

        let ageopt =
            if Array.length agearr >1 then
                Some (fst(agearr.[1])-fst(agearr.[0]))
            else
                None

        match ageopt with 
        | None -> 0L
        | Some elm -> (int64 elm)
    | [] -> failwith "unexpected"


let listUnfolder len list =
    if List.length list = len then
        None
    else
        let nextval = nextNum list
        Some (nextval, nextval::list)

let numlist len list =
    List.unfold (listUnfolder len) list

let calc1 list =
    let reslist = numlist 2020 list 
    match reslist with
    | [] -> failwith "unexpected"
    | ls-> List.last ls

let calc2 list =
    let reslist = numlist 300000 list 
    match reslist with
    | [] -> failwith "unexpected"
    | ls-> List.last ls