module LearnToFold

type NameAndSize= {Name:string;Size:int}

let maxNameAndSize list = 
   
   let innerMaxNameAndSize initialValue rest = 
       let action maxSoFar x = if maxSoFar.Size < x.Size then x else maxSoFar
       rest |> List.fold action initialValue 

   // handle empty lists
   match list with
   | [] -> 
       None
   | first::rest -> 
       let max = innerMaxNameAndSize first rest
       Some max


let fibonacciUnfolder max (f1,f2)  =
    if f1 > max then
        None
    else
        // return value and new threaded state
        let fNext = f1 + f2
        let newState = (f2,fNext)
        // f1 will be in the generated sequence
        Some (f1,newState)

let fibonacci max = List.unfold (fibonacciUnfolder max) (1,1)
//example call: 
//fibonacci 100