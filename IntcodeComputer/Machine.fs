﻿namespace IntcodeComputer

open FSharpx.Collections

module Machine =

    type ReturnMode =
        | RanToHalt
        | WaitingForInput
        | ProcessedInput
        | ProducedOutput
        | StepsRemaining

    type MachineStatus = 
        {
            PC:int;
            Memory:int64[];
            Inputs:Queue<int64>;
            ReturnMode:ReturnMode
        }

    let getArg1 (st:MachineStatus) =
        let pc=st.PC
        let opcode = st.Memory.[pc]
        let arg1Mode = 0
        match opcode with
        | 1L | 2L->
            let arg = st.Memory.[st.PC + 1]
            match arg1Mode with
            | 0 -> st.Memory.[int arg]
    //        	| 2 -> st.memory.[arg+st.RelativeBase];
            | _ -> arg
        | _ -> 0L

    let getArg2 (st:MachineStatus) =
        let pc=st.PC
        let opcode = st.Memory.[pc]
        let arg2Mode = 0
        match opcode with
        | 1L | 2L->
            let arg = st.Memory.[st.PC + 2]
            match arg2Mode with
            | 0 -> st.Memory.[int arg]
    //        	| 2 -> st.memory.[arg+st.RelativeBase];
            | _ -> arg
        | _ -> 0L


    let step st =
        let pc=st.PC
        let opcode = st.Memory.[pc]
        let (ret, newpc, inputs)=
            match opcode with
            | 1L ->
                let addrt = int st.Memory.[pc+3]
                st.Memory.[addrt] <- getArg1 st + getArg2 st
                (StepsRemaining, pc+4, st.Inputs)
            | 2L -> 
                let addrt = int st.Memory.[pc+3]
                st.Memory.[addrt] <- getArg1 st * getArg2 st
                (StepsRemaining, pc+4, st.Inputs)
            | 3L ->
                let addr31 = int st.Memory.[pc+1]
                if not st.Inputs.IsEmpty then
                    let (elm, tail) = st.Inputs.Uncons
                    st.Memory.[addr31]<-elm
                    (ProcessedInput, pc+2, tail)
                else
                    (WaitingForInput, pc, st.Inputs)
                (*
                						var addr31 = st.memory[st.pc + 1];
                						if (arg1Mode == 2)
                						{
                							addr31 = st.RelativeBase + addr31;
                						}
                						if (Inputs.Count > 0)
                						{
                							st.memory[addr31] = Inputs[0];
                							Inputs.RemoveAt(0);
                							st.pc = st.pc + 2;
                							if (calcMode == CalcMode.RunToFirstInput)
                							{
                								st.ReturnMode = ReturnMode.ProcessedInput;
                								return st;
                							}
                						}
                						else
                						{
                							st.ReturnMode = ReturnMode.WaitingForInput;
                							return st;
                						}
                *)
            | _ -> 
                (RanToHalt, pc, st.Inputs)
        {st with PC=newpc; ReturnMode = ret; Inputs = inputs}

    let rec run st =
        let st = step(st)
        if st.ReturnMode = RanToHalt then
            st
        else
            run st

    let readIntcode (s:string) =
        s.Split [|','|]
        |> Array.map (fun x-> x.Trim [|' '|] |> int64)

    let initMachine (s:int64[]) =
        {
            PC=0;
            Memory = s;
            ReturnMode = StepsRemaining;
            Inputs = Queue.empty
        }

    let initMachineFromString s =
        {
            PC=0;
            Memory = readIntcode s;
            ReturnMode = StepsRemaining;
            Inputs = Queue.empty
        }
    let initMachineFromStringWithInputs s inp=
        {
            PC=0;
            Memory = readIntcode s;
            ReturnMode = StepsRemaining;
            Inputs = Queue.ofList inp
        }

    let runFromString (s:string) =
        initMachineFromString s |> run

    let runFrom (s:int64[]) =
        initMachine s |> run