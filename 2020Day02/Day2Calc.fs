module Day2Calc

open System.IO

let Lines filename = 
    File.ReadLines filename

let aLines filename = 
    File.ReadLines filename
    |> Seq.map int32
    |> Seq.toArray

(*
1-3 a: abcde
1-3 b: cdefg (bad)
2-9 c: ccccccccc

3-12 n: grnxnbsmzttnzbnnn
*)

let numFrom (line:string) =
    let a=line.IndexOf('-')
    let c=line.Substring(0,a)
    int32 c

let numTo (line:string)=
    let a=line.IndexOf('-')+1
    let b=line.IndexOf(' ')-a
    let c=line.Substring(a,b)
    int32 c

let charMy (line:string)=
    let b=line.IndexOf(' ')
    let c=line.[b+1]
    c

let numMy (line:string) (my:char) =
    let a=
        line.ToCharArray()
        |> Array.filter (fun x-> x=my)
        |> Array.length
    a-1

        

let pwdValid (line:string) =
    let a=numFrom line
    let b=numTo line
    let my=charMy line
    let c=numMy line my
    (c>=a) && (c<=b) 

let strippedLine (line:string) =
    let a=line.IndexOf(':')
    let res=line.Substring(a+2)
    res

let pwdValid2 (line:string) =
    let a=numFrom line
    let b=numTo line
    let my=charMy line
    let s=strippedLine line
    let firstchar=s.[a-1]
    let secondchar=s.[b-1]
    let firstgood=my=firstchar
    let secondgood=my=secondchar
    let allgood = firstgood <> secondgood
    allgood

let linesValid lines =
    lines
    |> Seq.filter pwdValid
    |> Seq.length

let linesValid2 lines =
    lines
    |> Seq.filter pwdValid2
    |> Seq.length

