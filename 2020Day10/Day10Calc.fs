module Day10Calc

open System.IO

let Lines filename = 
    File.ReadLines filename
    |> Seq.map int
    |> Seq.toList

let sLines lines =
    0::(List.sort lines)


let hops lines =
    let slines = 
        lines
        |> sLines

    slines
    |> List.mapi
        (fun i x->
            if i<(List.length slines)-1 then
                slines.[i+1]-x
            else
                3
        )

let ones (lines:int list) =
    lines
    |> List.filter (fun x->x=1)
    |> List.length

let threes lines =
    lines
    |> List.filter (fun x->x=3)
    |> List.length
    
let calc1 filename =

    let shops =
        Lines filename
        |> hops

    let c1 = ones shops
    let c3 = threes shops
    c1 * c3

let rec countways adapters start goal memo =
    let k=(List.length adapters, start)
    if Map.containsKey k memo then
        memo, memo.[k]
    else
        let ways0 = 
            if goal - start <= 3 then
                1L
            else
                0L
        let memo1, ways1 =
            match adapters with
            | head::tail ->
                let memo2,ways2 =
                    if head - start <=3 then
                        countways tail head goal memo
                    else
                        memo, 0L
                let memo3, ways3 = countways tail start goal memo2
                memo3, ways2+ways3
            | [] -> memo, 0L

        let ways = ways0 + ways1
        (Map.add k ways memo1), ways //return

let calc2 filename =
    let lines =
        filename
        |> Lines
        |> List.sort

    let goal = (List.max lines) + 3

    let _, ways = countways lines 0 goal Map.empty

    ways