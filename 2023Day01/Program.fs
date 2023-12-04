// For more information see https://aka.ms/fsharp-console-apps

open System

printfn "Hello from F#"

[<Literal>]
let inputFile = __SOURCE_DIRECTORY__ + "\input.txt"

let lines = System.IO.File.ReadLines inputFile

module Part1 =
    let firstDigit (s:string) =
        s
        |> String.filter Char.IsDigit
        |> Seq.head
        |> string
        
    let lastDigit (s:string) =
        s
        |> String.filter Char.IsDigit
        |> (fun x->
                let arr = x.ToCharArray()
                let l = arr.Length
                arr[l-1]
                |> string
                )
        
    let cal s = firstDigit s + lastDigit s 

    let show1 () =
        lines
        |> Seq.map cal
        |> Seq.iter (printfn "%s")

    let calc1 () =
        lines
        |> Seq.map cal
        |> Seq.map Int32.Parse
        |> Seq.sum
        |> printfn "%d"

let targets =
    seq {
        ("0", 0)
        ("zero", 0)
        ("1", 1)
        ("one", 1)
        ("2", 2)
        ("two", 2)
        ("3", 3)
        ("three", 3)
        ("4", 4)
        ("four", 4)
        ("5", 5)
        ("five", 5)
        ("6", 6)
        ("six", 6)
        ("7", 7)
        ("seven", 7)
        ("8", 8)
        ("eight", 8)
        ("9", 9)
        ("nine", 9)
    }
    |> List.ofSeq
    
let reverseString (x:string) =
    let charArray = x.ToCharArray()
    Array.Reverse(charArray)
    let s = new string(charArray)
    s
    
let reversedTargets =
    targets
    |> List.map (fun (x, v)->
        reverseString x, v
    )
    
reversedTargets
|> List.iter (printfn "%A")

let posOfTarget (s:string) (t:string, v:int) =
    (s.IndexOf t), v
    
let firstDigit (s:string) =
    targets
    |> List.map (posOfTarget s)
    |> List.filter (fun (p,_) -> p >= 0)
    |> List.sortBy fst
    |> List.map snd
    |> List.toArray
    |> (fun  a ->
        a[0] * 10  
        )

let secondDigit (x:string) =
    let s = reverseString x
    reversedTargets
    |> List.map (posOfTarget s)
    |> List.filter (fun (p,_) -> p >= 0)
    |> List.sortBy fst
    |> List.map snd
    |> List.head
    
let lineValue s = firstDigit s + secondDigit s  

lines
|> Seq.map lineValue
|> Seq.iter (printfn "%d")

lines
|> Seq.map lineValue
|> Seq.sum
|> (printfn "Part2 %d")