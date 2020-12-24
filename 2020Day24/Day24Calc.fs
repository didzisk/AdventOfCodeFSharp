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

let rec calcTilesOneLine (currentWorld: Map<(int*int), bool>) (r:int) (c:int) (line:string)  : Map<(int*int), bool> =
        let (rplus, cplus), newline = command line
        let rnew = r+rplus
        let cnew = c+cplus
        if newline = String.Empty then
            if currentWorld.ContainsKey (rnew, cnew) then
                currentWorld |> Map.remove (rnew, cnew) 
            else
                currentWorld |> Map.add (rnew, cnew) true
        else
            calcTilesOneLine currentWorld rnew cnew newline

let folder worldSoFar line = 
    calcTilesOneLine worldSoFar 0 0 line

let calc1 filename =
    let lines = File.ReadLines filename

    let world = 
        lines
        |> Seq.fold folder Map.empty

    world 
    |> Map.iter (fun (r,c) _ ->  printfn "%d, %d" r c)

    world
    |> Map.count
