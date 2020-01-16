open System

type NodeDesc=
    {
        Code:string;
        HasSanta:bool;
        HasYou:bool;
    }
type tree =
    | Node of NodeDesc
    | Branch of tree

[<EntryPoint>]
let main argv =

    let inp1 = @"COM)B
B)C
C)D
D)E
E)F
B)G
G)H
D)I
E)J
J)K
K)L"

    let parseInput (s:string) =
        s.Split [|'\n'|]
        |> Array.map (
            fun x-> 
                let orb= x.Split [|')'|]
                let a = orb.[0]
                let b = (orb |> Array.last).Trim()
                a,b
                )
        |> Array.toList

    let rec parseInput1 p arr =
        match arr with
        | [] -> p
        | (parent,child)::xs -> 
            let p = p |> Map.add child parent
            parseInput1 p xs

    let parents = Input.Official |> parseInput |> parseInput1 Map.empty

    let rec distToRoot code (m: Map<string, string>) = 
        match code with 
        | "COM" -> 0
        | _ -> 1+distToRoot (Map.find code m) m
        
    let distances = 
        parents
        |> Map.map (
            fun child _ -> 
                let res=distToRoot child parents
                res
            )

    let nOrbits =
        distances
        |> Map.toSeq
        |> Seq.map (fun (_,dist)->dist)
        |> Seq.sum


    printfn "Part1: %A" nOrbits

    let rec fullParents code map =
        seq {
            if map |> Map.containsKey code then
                let parent = map |> Map.find code
                yield parent
                yield! fullParents parent map
        }

    let withDist code map=
        fullParents code map
        |> Seq.map (
            fun c -> 
                let res=distToRoot c parents
                res,c
            )
        |> Seq.rev

    fullParents "YOU" parents |> printfn "%A"
    fullParents "SAN" parents |> printfn "%A"

    withDist "YOU" parents |> printfn "%A"
    withDist "SAN" parents |> printfn "%A"
    
    let dcommon=
        Seq.zip  (withDist "YOU" parents) (withDist "SAN" parents)
        |> Seq.filter (fun ((_,by),(_,bss))->by=bss )
        |> Seq.map (fun ((ay,_),(_,_))->ay)
        |> Seq.max
    let dYou = distances |> Map.find "YOU"
    let dSan = distances |> Map.find "SAN"


    printfn "%A %A %A Part2: %A" dYou dSan dcommon (dYou-dcommon+dSan-dcommon-2)
    0 // return an integer exit code

