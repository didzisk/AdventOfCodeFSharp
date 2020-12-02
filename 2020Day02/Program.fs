// Learn more about F# at http://fsharp.org

open Day2Calc

[<EntryPoint>]
let main _ =
    printfn "Hello World from F#!"

    printfn "%A" (numFrom "32-")
    printfn "%A" (numTo "32-33 c: ccc")
    printfn "%A" (charMy "32-33 c: ccc")
    printfn "%A" (numMy "32-33 c: ccc" 'c')
    printfn "%A" (pwdValid "32-33 c: ccc")
    printfn "%A" (pwdValid "2-33 c: ccc")
    printfn "PwdValid2 %A" (pwdValid2 "1-3 a: abcde")
    printfn "PwdValid2 %A" (pwdValid2 "1-3 b: cdefg")
    printfn "PwdValid2 %A" (pwdValid2 "2-9 c: ccccccccc")

    Lines "Day2Input.txt"
    |> linesValid
    |> printfn"%A"

    Lines "Day2Input.txt"
    |> linesValid2
    |> printfn"%A"
    0 // return an integer exit code
