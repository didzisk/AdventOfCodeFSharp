module Day13Calc

open System
open System.IO
open Common

let Lines filename = 
    File.ReadLines filename
    |> Seq.toArray

let ReadId (lines:string array) =
    int64 lines.[0]

let ReadSchedule (lines:string array)  =
    lines.[1]
    |> split ","
    |> Array.filter (fun x-> x<>"x")
    |> Array.map int64

//int q = n / m;
//int n1 = m * q;
//int n2 = (n * m) > 0 ? (m * (q + 1)) : (m * (q - 1));
//if (abs(n - n1) < abs(n - n2))
//   return n1;
//return n2;

let NextDivisor (n:int64) (m:int64) =
    let q =n / m
    let n1 = m * q
    if n1 = n then
        n1
    else
        m * (q+1L)

let NextDeparture lines =
    let myId = ReadId lines
    lines
    |> ReadSchedule
    |> Array.map (fun m -> NextDivisor myId m)
    |> Array.min

let NextDepartureNum lines =
    let myId = ReadId lines
    lines
    |> ReadSchedule
    |> Array.minBy (fun m -> NextDivisor myId m)
    

let calc1 lines =
    let myId = ReadId lines
    let nextDep = NextDeparture lines
    let nextDepNum = NextDepartureNum lines
    (nextDep - myId) * nextDepNum

let Chinese arr =
    let rec sieve cs x N =
        match cs with
        | [] -> Some(x)
        | (a:int64,n:int64)::rest ->
            let arrProgress = Seq.unfold (fun x -> Some(x, x+N)) x
            let firstXmodNequalA = Seq.tryFind (fun x -> a = x % n)
            match firstXmodNequalA (Seq.take (int n) arrProgress) with
            | None -> None
            | Some(x) -> sieve rest x (N*n)

    arr 
    |> List.iter (fun congruences ->
        let cs =
            congruences
            |> List.map (fun (a:int64,n:int64) -> (a % n, n))
            |> List.sortBy (snd>>(~-)) 
        let an = List.head cs
        match sieve (List.tail cs) (fst an) (snd an) with
        | None    -> printfn "no solution"
        | Some(x) -> printfn "result = %d" x
    )


let calc2 (lines:string array) =
    let arr = lines.[1] |> split ","

    let numPos = Array.length arr |> int64

    let inputArray = 
        arr
        |> Array.mapi 
            (fun ii x->
                let i=int64 ii
                if x="x" then
                    numPos-i,0L
                else
                    numPos-i, (int64 x)
            )
        |> Array.filter (fun (i,x) -> x<>0L)
        |> Array.toList

    let input = [inputArray]
    input |> Chinese


let MI n g = //modular inverse
  let rec fN n i g e l a =
    match e with
    | 0 -> g
    | _ -> let o = n/e
           fN e l a (n-o*e) (i-o*l) (g-o*a) 
  (n+(fN n 1 0 g 0 1))%n

let rec gcd a b =
  if b = 0 
    then abs a
  else gcd b (a % b)
 
let CD n g = //chinese remainder
  match Seq.fold(fun n g->if (gcd n g)=1 then n*g else 0) 1 g with
  |0 -> None
  |fN-> Some ((Seq.fold2(fun n i g -> n+i*(fN/g)*(MI g ((fN/g)%g))) 0 n g)%fN)
 