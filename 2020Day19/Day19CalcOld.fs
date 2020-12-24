module Day19CalcOld

open System
open System.IO
open Common
open ParserLib

let Lines filename =
    File.ReadLines filename
    |> Seq.toArray


    //0: 4 1 5
    //1: 2 3 | 3 2
    //2: 4 4 | 5 5
    //3: 4 5 | 5 4
    //4: "a"
    //5: "b"
let rule5 = pchar 'b'
let rule4 = pchar 'a'
let rule3 = (rule4 .>>. rule5) <|> (rule5 .>>. rule4)
let rule2 = (rule4 .>>. rule4) <|> (rule5 .>>. rule5)
let rule1 = (rule2 .>>. rule3) <|> (rule3 .>>. rule2)
let rule0 = rule4 .>>. rule1 .>>. rule5 .>>. spaces1

//ababbb
//bababa
//abbbab
//aaabbb
//aaaabbb

//ababbb and abbbab match
//let calc1 =
//    run rule0 "ababbb"
//    |> printfn "match %A"

//    run rule0 "bababa"
//    |> printfn "fail %A"

//    run rule0 "abbbab"
//    |> printfn "match: %A"

//    run rule0 "abbb"
//    |> printfn "fail: %A"

//    run rule0 "aaaabbb"
//    |> (fun x ->
//            match x with
//            | Success (value,input) -> 
//                printfn "success %A" value
//            | Failure (label,error,pos) -> 
//                printfn "Error parsing %s\n%s at pos %A" label error pos
//                )
//    |> printfn "fail: %A"

type parserRec = {num:int; r1: int; r2: int; r3: int; r4:int}


let splitLines filename =
    File.ReadAllLines filename
    |> Array.map 
        (fun line->
            let args = 
                line 
                |> split ": |" 
                |> Array.map (replace "\"" String.Empty)
            if Array.length args = 1 ||Array.length args = 0 then
                ()
            else
                if args.[1] = "a" || args.[1]="b" then
                    printfn "let rule%d = pchar '%c'" (int args.[0]) args.[1].[0]
                else 
                    if Array.length args = 3 then
                        printfn "let rule%d = rule%d .>>. rule%d" (int args.[0]) (int args.[1]) (int args.[2])
                    else
                        if Array.length args = 5 then
                            printfn "let rule%d = (rule%d .>>. rule%d) <|> (rule%d .>>. rule%d)" (int args.[0]) (int args.[1]) (int args.[2]) (int args.[3]) (int args.[4])
                        else
                            ()

        )
let rule12 = pchar 'a'
let rule57 = pchar 'b'
let rule92 = rule57 .>>. rule57
let rule66 = rule57 .>>. rule12

(*
let splitLines1 filename =
    File.ReadAllLines filename
    |> Array.map 
        (fun line->
            let args = 
                line 
                |> split ": |" 
                |> Array.map (replace "\"" String.Empty)
 *)