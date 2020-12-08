module Day8Calc

open System.IO
open Common
open System.Text.RegularExpressions

let Lines filename = 
    File.ReadLines filename
    |> Seq.toArray

(*
nop +0  | 1
acc +1  | 2, 8(!)
jmp +4  | 3
acc +3  | 6
jmp -3  | 7
acc -99 |
acc +1  | 4
jmp -4  | 5
acc +6  |

*)


type MachineState = 
    {
        PC:int;
        Program:string[];
        //Inputs:List<int64>;
        VisitedLocations:Set<int>;
        Acc:int
        //RelativeBase:int;
        //Result:int64
        TerminatedNormally: Option<bool>
    }

let rec doOne (state:MachineState) : MachineState =
    let line = split " " state.Program.[state.PC]
    let cmd = line.[0]
    let arg0 = int32 line.[1]
    let pc =
        match cmd with
        | "jmp" -> state.PC + arg0
        | _ -> state.PC + 1
    let acc =
        match cmd with
        | "acc" -> state.Acc + arg0
        | _ -> state.Acc
    if pc<state.Program.Length then
        if state.VisitedLocations.Contains pc then
            {state with TerminatedNormally = Some false}
        else
            {state with 
                PC = pc; 
                VisitedLocations = state.VisitedLocations.Add pc; 
                Program = state.Program;
                Acc = acc
            }
            |> doOne  
    else
        {state with TerminatedNormally = Some true}

let calc1 lines =
    let ms:MachineState =
        {
            PC = 0;
            VisitedLocations = Set.empty |> Set.add 0;
            Program = lines;
            Acc = 0;
            TerminatedNormally = None
        }
    let res = doOne ms
    res.Acc

let rec calc2 place (lines:string array) =
    let line = split " " lines.[place]
    let cmd = line.[0]
    let arg0 = line.[1]
    let newcmd = 
        match cmd with
        | "jmp" -> "nop"
        | "nop" -> "jmp"
        | _ -> "acc"

    let newline = newcmd + " " + arg0

    printfn "Changed %d from %A to %s" place line newline

    let newLines = Seq.toArray lines
    Array.set newLines place newline

    let st = 
        {
            PC = 0;
            VisitedLocations = Set.empty |> Set.add 0;
            Program = newLines;
            Acc = 0;
            TerminatedNormally = None
        }

    let res = doOne st
    if res.TerminatedNormally = Some true then
        res.Acc
    else
        calc2 (place+1) lines 