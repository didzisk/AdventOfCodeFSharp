﻿// Learn more about F# at http://fsharp.org

open System
open IntcodeComputer.Machine
open GridForm

type Direction=
    | N
    | W
    | S
    | E

type Turn =
    | L
    | R


[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    let icode = "3,8,1005,8,327,1106,0,11,0,0,0,104,1,104,0,3,8,102,-1,8,10,1001,10,1,10,4,10,108,0,8,10,4,10,1001,8,0,28,1006,0,42,2,1104,11,10,1006,0,61,2,1005,19,10,3,8,1002,8,-1,10,1001,10,1,10,4,10,1008,8,1,10,4,10,102,1,8,65,1006,0,4,3,8,1002,8,-1,10,1001,10,1,10,4,10,108,1,8,10,4,10,1002,8,1,89,1,1108,10,10,1,1103,11,10,1,109,18,10,1006,0,82,3,8,102,-1,8,10,1001,10,1,10,4,10,108,0,8,10,4,10,102,1,8,126,2,109,7,10,1,104,3,10,1006,0,64,2,1109,20,10,3,8,1002,8,-1,10,101,1,10,10,4,10,108,1,8,10,4,10,101,0,8,163,3,8,102,-1,8,10,1001,10,1,10,4,10,108,1,8,10,4,10,1002,8,1,185,2,1109,12,10,2,103,16,10,1,107,11,10,3,8,102,-1,8,10,1001,10,1,10,4,10,108,0,8,10,4,10,1001,8,0,219,1,1005,19,10,3,8,102,-1,8,10,1001,10,1,10,4,10,108,1,8,10,4,10,102,1,8,245,2,1002,8,10,1,2,9,10,1006,0,27,1006,0,37,3,8,1002,8,-1,10,1001,10,1,10,4,10,108,0,8,10,4,10,102,1,8,281,1006,0,21,3,8,102,-1,8,10,101,1,10,10,4,10,108,0,8,10,4,10,1001,8,0,306,101,1,9,9,1007,9,1075,10,1005,10,15,99,109,649,104,0,104,1,21102,1,847069852568,1,21101,344,0,0,1105,1,448,21101,0,386979963688,1,21101,355,0,0,1105,1,448,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,21102,46346031251,1,1,21101,0,402,0,1105,1,448,21102,1,29195594775,1,21101,0,413,0,1105,1,448,3,10,104,0,104,0,3,10,104,0,104,0,21101,0,868498428772,1,21101,0,436,0,1106,0,448,21102,718170641172,1,1,21102,1,447,0,1105,1,448,99,109,2,21202,-1,1,1,21102,40,1,2,21102,1,479,3,21102,1,469,0,1105,1,512,109,-2,2105,1,0,0,1,0,0,1,109,2,3,10,204,-1,1001,474,475,490,4,0,1001,474,1,474,108,4,474,10,1006,10,506,1101,0,0,474,109,-2,2106,0,0,0,109,4,2102,1,-1,511,1207,-3,0,10,1006,10,529,21101,0,0,-3,22101,0,-3,1,22101,0,-2,2,21101,0,1,3,21101,548,0,0,1106,0,553,109,-4,2106,0,0,109,5,1207,-3,1,10,1006,10,576,2207,-4,-2,10,1006,10,576,21202,-4,1,-4,1106,0,644,22101,0,-4,1,21201,-3,-1,2,21202,-2,2,3,21102,1,595,0,1105,1,553,21201,1,0,-4,21101,0,1,-1,2207,-4,-2,10,1006,10,614,21102,1,0,-1,22202,-2,-1,-2,2107,0,-3,10,1006,10,636,22102,1,-1,1,21102,1,636,0,106,0,511,21202,-2,-1,-2,22201,-4,-2,-4,109,-5,2105,1,0"


    let rec nextStep (st:MachineStatus) m x y (dir:Direction)=
        if st.ReturnMode = RanToHalt then
            m
        else
            let col=
                if Map.containsKey (x,y) m then
                    m.Item (x,y)
                else
                    0L
            let stColor=runToOutput ignore {st with Inputs=[col]} //read color
            let map=m |> Map.add (x,y) stColor.Result 
            let stDir = runToOutput ignore stColor //read the turn
            let turn = 
                match stDir.Result with
                    | 0L -> Turn.L
                    | _  -> Turn.R
            let newDir=
                match (dir,turn) with
                | (N,Turn.L) -> W
                | (W,Turn.L) -> S
                | (S,Turn.L) -> E
                | (E,Turn.L) -> N
                | (N,Turn.R) -> E
                | (W,Turn.R) -> N
                | (S,Turn.R) -> W
                | (E,Turn.R) | _ -> S
            let (newx, newy) = 
                match newDir with
                | N -> (x, y+1)
                | W -> (x-1, y)
                | S -> (x, y-1)
                | E -> (x+1,y)
            nextStep stDir map newx newy newDir 
    let st = initMachineFromStringWithInputs icode [0L]
    //initial st holds color (0=black) under (0,0)
    let points=nextStep st Map.empty 0 0 N

    points |> Seq.length |> printfn "%A" 

    let m =
        Map.empty
        |> Map.add (0,0) 1L
    let points2=nextStep st m 0 0 N
    points2
    |> Map.iter (
        fun (x,y) col-> 
            let color = 
                if col=1L then 
                    System.Drawing.Color.Blue 
                else
                    System.Drawing.Color.White 
            GridForm.SmallPoint GridForm.mf 10.0 20.0 320.0 color (float x) (float y)
        )
    Console.ReadLine()|>ignore
    0 // return an integer exit code
