module Day24Calc

open System
open System.IO

let command (line:string) =
    if line.[0] = 'w' then
        (0,-1), line.Substring(1)
    else
        if line.[0] = 'e' then
            (0,1), line.Substring(1)
        else
            if line.Substring(0,2) = "nw" then
                (-1,0), line.Substring(2)
            else
                if line.Substring(0,2) = "ne" then
                    (-1,1), line.Substring(2)
                else
                    if line.Substring(0,2) = "sw" then
                        (1,-1), line.Substring(2)
                    else
                        if line.Substring(0,2) = "se" then
                            (1,0), line.Substring(2)
                        else
                            failwith "Unexpected command"

let rec calcTilesOneLine (currentWorld: Set<(int*int)>) (r:int) (c:int) (line:string)  : Set<(int*int)> =
        let (rplus, cplus), newline = command line
        let rnew = r+rplus
        let cnew = c+cplus
        if newline = String.Empty then
            if currentWorld.Contains (rnew, cnew) then
                currentWorld |> Set.remove (rnew, cnew) 
            else
                currentWorld |> Set.add (rnew, cnew)
        else
            calcTilesOneLine currentWorld rnew cnew newline

let folder worldSoFar line = 
    calcTilesOneLine worldSoFar 0 0 line

let calc1 filename =
    let lines = File.ReadLines filename

    let world = 
        lines
        |> Seq.fold folder Set.empty

    world 
    |> Set.iter (fun (r,c)->  printfn "%d, %d" r c)

    world
    |> Set.count

let lifeOne (oldWorld: Set<(int*int)>) (newWorld: Set<(int*int)>) (r:int) (c:int) =
    let neighbors = [|(0,-1); (0,1); (-1,0); (1,-1); (1,0)|]
    
    let numNeighbors =
        neighbors
        |> Array.filter 
            (fun (rplus, cplus) ->
                oldWorld.Contains (r+rplus, c+cplus)
            )
        |> Array.length

let limits (oldWorld: Set<(int*int)>) =
    let minrow =
        oldWorld
        |> Set.map 
            (fun (r,_)-> r)
        |> Set.minElement

    (*
Any black tile with zero or more than 2 black tiles immediately adjacent to it is flipped to white.
Any white tile with exactly 2 black tiles immediately adjacent to it is flipped to black.    
    *)

    if oldWorld.Contains (r,c) then
        if numNeighbors = 0 || numNeighbors >=2 then
            newWorld.Remove (r,c)
        else
            newWorld.Add (r,c)
    else
        if numNeighbors = 2 then
            newWorld.Add (r,c)
        else
            oldWorld