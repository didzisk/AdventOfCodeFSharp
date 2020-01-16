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
    let treeAddOrbit (t:tree) a b =
        
        t

    let parents=Map.empty
    let children=Map.empty
    let parseInput (s:string) =
        s.Split [|'\n'|]
        |> Array.map (
            fun x-> 
                let orb= x.Split [|')'|]
                let a = orb.[0]
                let b = (orb |> Array.last).Trim()
                printfn "%s, %s" a b
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
        
    parseInput inp1 |> List.length |> printfn "%A"

    let distances = 
        parents
        |> Map.map (
            fun child _ -> 
                let res=distToRoot child parents
                printfn "%A:%A" child res
                res
            )

    let nOrbits =
        distances
        |> Map.toSeq
        |> Seq.map (fun (_,dist)->dist)
        |> Seq.sum


    printfn "Part1: %A" nOrbits

    
        

    0 // return an integer exit code
