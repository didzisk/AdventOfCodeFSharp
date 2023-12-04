open System
open System.IO
open Microsoft.FSharp.Core
open Utils.Downloader

[<Literal>]
let inputFile = __SOURCE_DIRECTORY__ + "\input.txt"

if not (File.Exists inputFile) then 
    downloadInput __SOURCE_DIRECTORY__

let text = @"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"
let linesEx = text.Split([|'\n'|]);
let lines =
    File.ReadLines inputFile |> Array.ofSeq

let parseValues (s:string) =
        s.Split([|' '|])
        |> Array.choose (fun x->
            try
                Some (Int32.Parse x)
            with
                | _->None
            )

let parseLine (s:string) =
    let arr = s.Split([|':';'|'|])
    let w = parseValues arr[1]
    let c = parseValues arr[2]
    w, c

let folder1 c s x =
    if Array.contains x c then
        if s = 0 then
            1
        else
            s * 2
    else
        s
    
let countLineScore folder (w:int array) =
    (0, w) ||> Array.fold folder
    
let countLineScore1 (w:int array, c:int array) =
    countLineScore (folder1 c) w
    
let mapToLineScores = Array.map (parseLine >> countLineScore1)

let folder2 c s x =
    if Array.contains x c then
        s + 1
    else
        s

let countLineScore2 (w:int array, c:int array) =
    countLineScore (folder2 c) w
    
let mapToLineScores2 =
    Array.map (parseLine >> countLineScore2)

let calc l =
    l
    |> mapToLineScores
    |> Array.sum
    |> (printfn "Part1: %A")
    
    let arr = mapToLineScores2 l
        
    let newWorld = Array.init arr.Length (fun _->1)
    
    for i = 0 to arr.Length-1 do
        let x = arr[i]
        for j = 1 to x do
            newWorld[i+j]<-newWorld[i+j]+newWorld[i]
       
    newWorld
    |> Array.sum
    |> (printfn "Part2: %A")
    
calc lines
