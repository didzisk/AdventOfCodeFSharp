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




