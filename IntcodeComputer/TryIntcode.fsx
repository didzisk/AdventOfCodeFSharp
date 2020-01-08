

#r "bin\Debug\\netcoreapp3.0\IntcodeComputer.dll"

open IntcodeComputer
let st:Machine.MachineStatus = {PC=0; Memory = [|1L;9L;10L;3L;2L;3L;11L;0L;99L;30L;40L;50L|]; ReturnMode=Machine.StepsRemaining}

let stnew=Machine.run st

Machine.initMachineFromString "1,0,0,0,99" |> Machine.run

Machine.runFromString "2,3,0,3,99"
Machine.runFromString "2,4,4,5,99,0"
Machine.runFromString "1,1,1,4,99,5,6,0,99"

Machine.runFromString "3,0,4,0,99"