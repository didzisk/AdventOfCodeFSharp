module Day03Calc1

open System
open System.IO

let numZeros rows pos =
    rows
    

type StatusReport = 
    {
        Zeros:int
        Ones:int
    }

let initArr = [|{Zeros = 0;Ones = 0};{Zeros = 0;Ones = 0};{Zeros = 0;Ones = 0};
    {Zeros = 0;Ones = 0};{Zeros = 0;Ones = 0};{Zeros = 0;Ones = 0};
    {Zeros = 0;Ones = 0};{Zeros = 0;Ones = 0};{Zeros = 0;Ones = 0};
    {Zeros = 0;Ones = 0};{Zeros = 0;Ones = 0};{Zeros = 0;Ones = 0};|]
    
    

let folder1 numDigits (state:StatusReport[]) line =
    seq{
        for i = 0 to numDigits - 1 do
            let mask = 1 <<< i
            let newElm =
                match line &&& mask with
                | 0 -> { state[i] with Zeros = 1 + state[i].Zeros} 
                | _ -> { state[i] with Ones = 1 + state[i].Ones} 
            yield newElm
    }
    |> Seq.toArray
        

let Lines filename =
    let all =
        File.ReadLines filename
    let numDigits = 
        all
        |> Seq.head
        |> String.length
    (
    all
        |> Seq.map(fun s-> "0b"+s)
        |> Seq.map int32
        , numDigits
        )

let Calc1 filename = 
    let (all, numDigits) = Lines filename

    let status = 
        all
        |> Seq.fold (folder1 numDigits) initArr
    
    let epsilon = 
        seq{
            for i = 0 to numDigits - 1 do
                let mask = 1 <<< i
                if status[i].Zeros > status[i].Ones then
                    yield mask
                else
                    yield 0

        }
        |> Seq.sum
    let gamma = 
        seq{
            for i = 0 to numDigits - 1 do
                let mask = 1 <<< i
                if status[i].Zeros < status[i].Ones then
                    yield mask
                else
                    yield 0

        }
        |> Seq.sum

    gamma * epsilon, $"gamma {gamma}" , $"epsilon {epsilon}"

let folder2 numDigits (a:list<int32>) i =

    if a.Length = 1 then
        a
    else
        let statusReports = 
            a
            |> Seq.fold (folder1 (numDigits)) initArr

        let statusReport = statusReports[numDigits-i]

        let mask = 1<<<(numDigits-i)

        if statusReport.Zeros > statusReport.Ones then
            a
            |> List.filter (
                fun x-> 
                    x &&& mask = 0
                )
        else
            a
            |> List.filter (
                fun x-> 
                    x &&& mask <> 0
                )

let folder3 numDigits (a:list<int32>) i =

    if a.Length = 1 then
        a
    else
        let statusReports = 
            a
            |> Seq.fold (folder1 (numDigits)) initArr

        let statusReport = statusReports[numDigits-i]

        let mask = 1<<<(numDigits-i)

        if statusReport.Zeros > statusReport.Ones then
            a
            |> List.filter (
                fun x-> 
                    x &&& mask <> 0
                )
        else
            a
            |> List.filter (
                fun x-> 
                    x &&& mask = 0
                )

let CalcOxygen filename = 
    let (all, numDigits) = Lines filename

    let status = 
        all
        |> Seq.fold (folder1 numDigits) initArr

    let oneFolder = folder2 numDigits

    [1..numDigits]
    |> List.fold oneFolder (Seq.toList(all))

let CalcCO2 filename = 
    let (all, numDigits) = Lines filename

    let status = 
        all
        |> Seq.fold (folder1 numDigits) initArr

    let oneFolder = folder3 numDigits

    [1..numDigits]
    |> List.fold oneFolder (Seq.toList(all))

