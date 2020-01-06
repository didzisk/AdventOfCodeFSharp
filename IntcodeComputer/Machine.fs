namespace IntcodeComputer

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
        let (ret, newpc)=
            match opcode with
            | 1L ->
                let addrt = int st.Memory.[pc+3]
                st.Memory.[addrt] <- getArg1 st + getArg2 st
                (StepsRemaining, pc+4)
            | 2L -> 
                let addrt = int st.Memory.[pc+3]
                st.Memory.[addrt] <- getArg1 st * getArg2 st
                (StepsRemaining, pc+4)
            | _ -> 
                (RanToHalt, pc)
        {st with PC=newpc; ReturnMode = ret}

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
            ReturnMode = StepsRemaining
        }

    let initMachineFromString (s:string) =
        {
            PC=0;
            Memory = readIntcode s;
            ReturnMode = StepsRemaining
        }

    let runFromString (s:string) =
        initMachineFromString s |> run

    let runFrom (s:int64[]) =
        initMachine s |> run