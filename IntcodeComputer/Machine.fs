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
            Inputs:List<int64>;
            ReturnMode:ReturnMode;
            RelativeBase:int
        }

    let getArg1 (st:MachineStatus) =
        let pc=st.PC
        let code = int st.Memory.[pc]
        let opcode = code % 100
        let arg1Mode = (code % 1000 - code % 100) / 100
        match opcode with
        | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9->
            let arg = st.Memory.[st.PC + 1]
            match arg1Mode with
            | 0 -> st.Memory.[int arg]
            | 2 -> st.Memory.[int arg + st.RelativeBase];
            | _ -> arg
        | _ -> 0L

    let getArg2 (st:MachineStatus) =
        let pc=st.PC
        let code = int st.Memory.[pc]
        let opcode = code % 100
        let arg2Mode = (code % 10000 - code % 1000) / 1000
        match opcode with
        | 1 | 2 | 5 | 6 | 7 | 8 ->
            let arg = st.Memory.[st.PC + 2]
            match arg2Mode with
            | 0 -> st.Memory.[int arg]
            | 2 -> st.Memory.[int arg + st.RelativeBase];
            | _ -> arg
        | _ -> 0L


    let step st fo =
        let pc=st.PC
        let opcode = int st.Memory.[pc] % 100
        let (ret, newpc, inputs, rbase)=
            match opcode with
            | 1 ->
                let addrt = int st.Memory.[pc+3]
                st.Memory.[addrt] <- getArg1 st + getArg2 st
                (StepsRemaining, pc+4, st.Inputs, st.RelativeBase)
            | 2 -> 
                let addrt = int st.Memory.[pc+3]
                st.Memory.[addrt] <- getArg1 st * getArg2 st
                (StepsRemaining, pc+4, st.Inputs, st.RelativeBase)
            | 3 ->
                let addr31 = int st.Memory.[pc+1]
                match st.Inputs with
                | head :: tail -> 
                    st.Memory.[addr31]<-head
                    (ProcessedInput, pc+2, tail, st.RelativeBase)
                | [] -> (WaitingForInput, pc, st.Inputs, st.RelativeBase)
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
            | 4 ->
                getArg1 st |> fo
                (StepsRemaining, pc+2, st.Inputs, st.RelativeBase)
            | 5 -> //JNZ
                if getArg1 st <> 0L then
                    (StepsRemaining, int (getArg2 st), st.Inputs, st.RelativeBase)
                else
                    (StepsRemaining, pc+3, st.Inputs, st.RelativeBase)
            | 6 -> //JZ
                if getArg1 st = 0L then
                    (StepsRemaining, int (getArg2 st), st.Inputs, st.RelativeBase)
                else
                    (StepsRemaining, pc+3, st.Inputs, st.RelativeBase)
            | 7 -> //LT 
                let code = int st.Memory.[pc]
                let addrt = 
                    if code / 10000 =2 then
                        int st.Memory.[int st.PC + 3]
                    else
                        int st.Memory.[int st.PC + 3] + st.RelativeBase
                if getArg1 st < getArg2 st then
                    st.Memory.[addrt] <- 1L
                else
                    st.Memory.[addrt] <- 0L
                (StepsRemaining, pc+4, st.Inputs, st.RelativeBase)
            | 8 -> //EQ
                let code = int st.Memory.[pc]
                let addrt = 
                    if code / 10000 =2 then
                        int st.Memory.[int st.PC + 3]
                    else
                        int st.Memory.[int st.PC + 3] + st.RelativeBase
                if getArg1 st = getArg2 st then
                    st.Memory.[addrt] <- 1L
                else
                    st.Memory.[addrt] <- 0L
                (StepsRemaining, pc+4, st.Inputs, st.RelativeBase)
            | 9 -> //set rel base
                (StepsRemaining, pc+2, st.Inputs, st.RelativeBase+int (getArg1 st))
            | _ -> 
                (RanToHalt, pc, st.Inputs, st.RelativeBase)
        {st with PC=newpc; ReturnMode = ret; Inputs = inputs; RelativeBase = rbase}

    let rec run fo st =
        let st = step st fo
        if st.ReturnMode = RanToHalt then
            st
        else
            run fo st
 
    let readIntcode (s:string) =
        s.Split [|','|]
        |> Array.map (fun x-> x.Trim [|' '|] |> int64)

    let initMachine (s:int64[]) =
        {
            PC=0;
            Memory = s;
            ReturnMode = StepsRemaining;
            Inputs = [];
            RelativeBase = 0
        }

    let initMachineFromString s =
        {
            PC=0;
            Memory = readIntcode s;
            ReturnMode = StepsRemaining;
            Inputs = [];
            RelativeBase = 0
        }
    let initMachineFromStringWithInputs s inp=
        {
            PC=0;
            Memory = readIntcode s;
            ReturnMode = StepsRemaining;
            Inputs = inp;
            RelativeBase = 0
        }

    let runFromString (s:string) fo =
        initMachineFromString s |> run fo

    let runFromStringWithInputs s inp fo =
        initMachineFromStringWithInputs s inp |> (run fo)

    let runFrom (s:int64[]) fo =
        initMachine s |> run fo