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

let ScanExample =
    let initialBalance = 1122.73
    let transactions = [| -100.00; +450.34; -62.34; -127.00; -13.50; -12.92 |]
    let balances =
        Array.scan (fun balance transactionAmount -> balance + transactionAmount) initialBalance transactions
    printfn "Initial balance:\n $%10.2f" initialBalance
    printfn "Transaction   Balance" 
    for i in 0 .. Array.length transactions - 1 do
        printfn "$%10.2f $%10.2f" transactions.[i] balances.[i]
    printfn "Final balance:\n $%10.2f" balances.[ Array.length balances - 1]