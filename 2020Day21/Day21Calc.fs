module Day21Calc

open System
open System.IO
open Common

let Lines filename =
    File.ReadLines filename

type FieldDesc =
    {
        FieldName:string;
        Range1a:int;
        Range1b:int;
        Range2a:int;
        Range2b:int;
    }

let readFieldDesc (line:string)  : FieldDesc = 
    let colonpos = line.IndexOf(':')
    let fieldName = line.Substring(0,colonpos)
    let rest = 
        split " or-" (line.Substring(colonpos+1))
    let range1a = (int rest.[0])
    let range1b = (int rest.[1])
    let range2a = (int rest.[2])
    let range2b = (int rest.[3])

    {
        FieldName = fieldName
        Range1a = range1a
        Range1b = range1b
        Range2a = range2a
        Range2b = range2b
    }

let readFields filename =
    let lines = 
        Lines filename
        |> Seq.toArray

    let lineyour = 
        lines
        |> Array.findIndex (fun x-> x.StartsWith("your ticket"))
    let fields =
        lines
        |> Array.take (lineyour-1)
        |> Array.map readFieldDesc
    fields

let readLines filename =
    let lines = 
        Lines filename
        |> Seq.toArray

    let linenearby = 
        lines
        |> Array.findIndex (fun x-> x.StartsWith("nearby tickets"))

    lines
    |> Array.skip (linenearby+1)

let validForThisField (x:int) (field:FieldDesc)=
     ((x>=field.Range1a && x<=field.Range1b) || (x>=field.Range2a && x<=field.Range2b))

let invalidValuesSum (fields: FieldDesc array) line =
    let lineValues = 
        split "," line
        |> Array.map int
    lineValues
        |> Array.filter
            (fun x ->
                fields 
                |> Array.exists (validForThisField x)
                |> not
            )
        |> Array.sum

let calc1 filename = 
    let fields = readFields filename
    let lines = readLines filename

    lines
    |> Array.map (invalidValuesSum fields)
    |> Array.sum

let show1 filename = 
    let fields = readFields filename
    let lines = readLines filename

    lines
    |> Array.map (invalidValuesSum fields)
    |> printfn "%A"

let hasAllValidValues (fields: FieldDesc array) line =
    let lineValues = 
        split "," line
        |> Array.map int
    lineValues
        |> Array.exists
            (fun x ->
                fields 
                |> Array.exists (validForThisField x)
                |> not
            )
        |> not

let validLines (fields: FieldDesc array) (lines:string array) =
    lines
    |> Array.filter (hasAllValidValues fields)

let show2 filename =
    let fields = readFields filename
    let lines = readLines filename

    validLines fields lines
    |> printfn "%A"

let ruleGoodForThisPos  (field: FieldDesc) (thisIndex:int) (lineValues:int array) = 
    validForThisField lineValues.[thisIndex] field

let rulePosGoodForAllLines (lines:string array) (field: FieldDesc) (pos:int) = 
    lines
    |> Array.map 
        (fun line ->
            line
            |> split ","
            |> Array.map int
            |> (ruleGoodForThisPos field pos)
        )
    |> Array.exists (fun x-> not x)
    |> not


let ruleGoodForAllLines (lines:string array) (field: FieldDesc) = 
    let numFields = 
        split "," lines.[0]
        |> Array.length

    let indArr = [|0..numFields-1|]

    let pos =
        indArr
        |> Array.filter (rulePosGoodForAllLines lines field)
    pos
    
let rulePositions (lines:string array) (fields: FieldDesc array) =
    fields
    |> Array.map 
        (fun x -> (ruleGoodForAllLines lines x), x)


let show21 filename =
    let fields = readFields filename
    let rawlines = readLines filename

    let lines = validLines fields rawlines

    rulePositions lines fields
    |> Array.iter 
        (fun (a,r) ->
            printfn "num: %A rule:%A" a r.FieldName
        )

//after manual compression:
//num: 18 rule:"departure time"
//num: 6 rule:"departure date"
//num: 14 rule:"departure location"
//num: 3 rule:"departure station"
//num: 15 rule:"departure track"
//num: 13 rule:"departure platform"

let calc2 = 
    let arr =
        "151,71,67,113,127,163,131,59,137,103,73,139,107,101,97,149,157,53,109,61"
        |> split ","
        |> Array.map int
    (int64 arr.[18]) * (int64 arr.[6]) * (int64 arr.[14]) * (int64 arr.[3]) * (int64  arr.[15]) * (int64  arr.[13])
    |> printfn "%d"

let calcAllIndices filename =
    let fields = readFields filename
    let rawlines = readLines filename

    let lines = validLines fields rawlines

    let arr = 
        rulePositions lines fields
        |> Array.sortByDescending (fun (a,_) -> Array.length a)

    arr
    |> Array.mapi (
        fun i (a,r) -> 
            let allMy = Set.ofArray a
            let allNext = 
                if i+1 < (Array.length arr) then
                    Set.ofArray (fst arr.[i+1])
                else
                    Set.empty
            let my = 
                (allMy - allNext)
                |> Set.minElement
            my, r
        )

let show22 filename =
    calcAllIndices filename
    |> Array.iter (fun (a,r) ->
         printfn "num: %A rule:%A" a r.FieldName
     )

let calc22 filename =
    let arr =
        calcAllIndices filename
        |> Array.filter(fun (_,r)-> r.FieldName.StartsWith ("departure"))
        |> Array.map (fun (a,_) -> a )

    let lines = 
        Lines filename
        |> Seq.toArray
    let lineyour = 
        lines
        |> Array.findIndex (fun x-> x.StartsWith("your ticket"))
    let line = lines.[lineyour+1]
    let values = 
        line 
        |> split ","
        |> Array.map int64
    let prod =
        arr
        |> Array.map (fun x-> values.[x])
        |> Array.fold (*) 1L
    prod
