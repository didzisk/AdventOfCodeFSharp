// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    let lSize = 25*6
    let len=Input.Official.Length
    let numLayers=len/lSize
    let layers=
        seq{
            for i=0 to numLayers-1 do
                yield Input.Official.Substring(i*lSize, lSize)
        }

    let numC c s = 
        s 
        |> String.filter (fun x->x=c)
        |> String.length

    let zero = numC '0'
    let one = numC '1'
    let two = numC '2'


    let minL=
        layers
        |> Seq.minBy zero

    let ones=minL |> one
    let twos=minL |> two

    printfn "%A" (ones*twos)

    let calcColors s1 s2 =
        let s=
            Seq.zip s1 s2
            |> Seq.map(fun (a,b)-> if a='2' then b else a)
            |> Seq.toArray
        new string (s)

    let s = calcColors "002" "111"
    printfn "002 111 %A" (calcColors "002" "111")


    let finalLayer=
        layers
        |> Seq.reduce calcColors

    for i=0 to 5 do
        printfn ""
        for k=0 to 24 do
            if (finalLayer.Substring(i*25, 25).[k])='1' then
                printf "#"
            else
                printf " "



    0
