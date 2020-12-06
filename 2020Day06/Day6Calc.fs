module Day6Calc

open System.IO
open Common

let Lines filename = 
    File.ReadLines filename
    |> Seq.toArray

let answers lines =
    let mutable s = ""
    seq {
        for l in lines do
            if l = "" then
                yield s
                s <- ""
            else
                s <- s+l
        yield s
        }

let uniqueAnswers (a:string) =
    a.ToCharArray()
    |> Set.ofArray
    |> Set.count

let sumUniqueAnswers (inp:string seq) =
    inp
    |> Seq.map uniqueAnswers
    |> Seq.sum

let part1 filename =
    Lines filename
    |> answers
    |> sumUniqueAnswers

    
let answerSets lines =
    let mutable s = List.empty
    seq {
        for l in lines do
            if l = "" then
                yield s
                s <- List.empty
            else
                let newSet=
                    l.ToCharArray()
                    |> Set.ofArray
                s<-newSet::s
                
        yield s
        }

let innerEqualAnswers first (s:Set<char> list)  =
    let action resultSoFar x = Set.intersect resultSoFar x
    s |> List.fold action first
    |> Set.count

let equalAnswers (s:Set<char> list) =
    match s with
    | [] -> 
        0
    | first::rest -> 
        let max = innerEqualAnswers first rest
        max


  
let part2 filename =
    Lines filename
    |> answerSets
    |> Seq.map equalAnswers
    |> Seq.sum
    |> printfn "%A"

  