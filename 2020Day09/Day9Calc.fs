module Day9Calc

open System.IO

let Lines filename = 
    File.ReadLines filename
    |> Seq.map int64
    |> Seq.toArray

let preamble (start:int) (prlen:int) (lines:int64 array) =
    Array.sub lines (start-prlen) prlen
    |> Set.ofArray
    |> Set.toArray

let preambleSums preamble =
    Array.allPairs preamble preamble
    |> Array.map (fun (a,b)-> a+b)
    |> Set.ofArray

let validElm (i:int) (prlen:int) (lines:int64 array) =
    if i<prlen then
        -1
    else
        let pr = preamble i prlen lines
        let sums = (preambleSums pr)
        if Set.contains lines.[i] sums then 
            1
        else
            0

let calc1 filename (prlen:int) =
    let arr=Lines filename

    arr
    |> Array.mapi (fun i x -> (validElm i prlen arr, x))
    |> Array.filter (fun (x,_) -> x=0)
    |> Array.map (fun (_,a) -> a)

(*
currSum = arr[0] 
start = 0
i = 1
while i <= len(arr): 
    while currSum > targetSum and start < i-1: 
        currSum = currSum - arr[start] 
        start += 1

    if currSum == targetSum: 
        subarr = arr[start:i]
        return min(subarr) + max(subarr)

    if i < len(arr): 
        currSum = currSum + arr[i] 
    i += 1
return None
*)
let subarray lines start len =
    Array.sub lines start len

let calc2sum i j (lines:int64 array) =
    subarray lines i j
    |> Array.sum

let calcSums (lines:int64[]) =
    let len = Array.length lines
    seq {
        for i = 0 to len-1 do
            for j = 1 to len-1-i do
                yield ((calc2sum i j lines), i, j)
    }

let calc2 filename expected =
    let lines = Lines filename
    
    lines
    |> calcSums
    |> Seq.filter (fun (a,i,j) -> a=expected)
    |> Seq.map (fun (a,i,j) -> 
        let sortedSub=
            subarray lines i j
            |> Array.sort
        let slen = Array.length sortedSub
        sortedSub.[0]+sortedSub.[slen-1]
        )
//        (i,j,lines.[i]+lines.[i+j]))
 