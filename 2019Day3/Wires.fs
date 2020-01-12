module Wires

type point =
    {
    X : int;
    Y : int;
    dist:int;
    }

let simpleFolder _ p = 
    printfn "%A" p
    p

let simplefold (s:string) = 
    let arr = 
        s.Split [|','|]
        |> Array.map (fun x-> x.Trim [|' '|])
    arr |> Array.fold simpleFolder ""

let wire (s:string) :point list =
    let arr = 
        s.Split [|','|]
        |> Array.map (fun x-> x.Trim [|' '|])

    let folder (currentPoint,currentList) elm =

        let unfolder (point, command:string) = 
            let dir=command.[0..0]
            let num = int command.[1..] - 1
            if num=(-1) then
                None
            else
                let nextCommand=dir+string num
                let nextPoint=
                    match dir with 
                    | "R" -> ({point with X=point.X+1; dist = point.dist+1})
                    | "L" -> ({point with X=point.X-1; dist = point.dist+1})
                    | "U" -> ({point with Y=point.Y-1; dist = point.dist+1})
                    | _   -> ({point with Y=point.Y+1; dist = point.dist+1})
                Some (nextPoint,(nextPoint, nextCommand))
        let kk= List.unfold unfolder (currentPoint, elm)
        
        (Seq.last kk, currentList @ kk)
    let (st,gg)=arr |> Array.fold folder ({X=0; Y=0; dist=0},[])
    gg
    