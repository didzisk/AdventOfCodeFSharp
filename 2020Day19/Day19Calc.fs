module Day19Calc

open System
open System.IO
open Common

let Lines filename =
    File.ReadLines filename
    |> Seq.toArray



//ababbb
//bababa
//abbbab
//aaabbb
//aaaabbb

type AndThenPair = {pt1: int; andThen: int}

type MyAlternative = 
    | Text of String
    | Alternatives of MyAlternative seq
    | Unresolved
    | Empty

type parserRec = {num:int; one: AndThenPair option ; two: AndThenPair option; left:(MyAlternative * MyAlternative); right:(MyAlternative* MyAlternative)}



let splitLines filename =
    seq{
        for line in File.ReadAllLines filename do
                let args = 
                    line 
                    |> split ": |" 
                    |> Array.map (replace "\"" String.Empty)
                if Array.length args = 1 ||Array.length args = 0 then
                    ()
                else
                    if args.[1] = "a" || args.[1]="b" then
                        printfn "rule%d = '%c'" (int args.[0]) args.[1].[0]
                        yield {num=int args.[0]; one = None; two = None; left = (Text args.[1], Empty); right = (Empty, Empty)}
                    else 
                        if Array.length args = 3 then //"and" only
                            printfn "rule%d = rule%d andThen rule%d" (int args.[0]) (int args.[1]) (int args.[2])
                            yield {num=int args.[0]; one = Some {pt1 = int args.[1]; andThen = int args.[2]}; two = None; left = (Unresolved, Unresolved); right = (Empty, Empty)}

                        else
                            if Array.length args = 5 then//"andPair" or "andPair"
                                printfn "let rule%d = (rule%d .>>. rule%d) <|> (rule%d .>>. rule%d)" (int args.[0]) (int args.[1]) (int args.[2]) (int args.[3]) (int args.[4])
                                yield {num=int args.[0]; 
                                    one = Some {pt1 = int args.[1]; andThen = int args.[2]}; 
                                    two = Some {pt1 = int args.[3]; andThen = int args.[4]};
                                    left = (Unresolved, Unresolved); right = (Unresolved, Unresolved)}
                            else
                                ()

    }

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