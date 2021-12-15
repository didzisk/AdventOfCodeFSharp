module Day14Calc2


let treeWorld (originalWorld:string) = 
    originalWorld
    |> Seq.pairwise
    |> Seq.groupBy (fun x->x)
    |> Seq.map(fun (x,s) -> x, int64 (Seq.length s))
    |> Map.ofSeq

let addOnePair (x:char,y: char) (thisCount:int64) (newMap:Map<(char * char),int64>) =
    let oldCount =
        if newMap |> Map.containsKey (x,y) then
            newMap.[(x,y)]
        else
            0
    newMap.Add((x,y),thisCount+oldCount)

let processOnePair (commands:seq<Day14Calc.Command>)  (newMap:Map<(char * char),int64>) (x:char,y: char) (thisCount:int64) =
    let command = 
        commands
        |> Seq.tryFind (fun {Pair = (pairLeft, pairRight); InChar = _} -> pairLeft = x && pairRight = y)
    match command with
    | Some {Pair = (_,_); InChar = inChar} -> 
        newMap
        |> addOnePair (x,inChar) thisCount
        |> addOnePair (inChar,y) thisCount
    | _ -> addOnePair (x,y) thisCount newMap 
    

let rec nextWorld  (commands:seq<Day14Calc.Command>) counter (old:Map<(char * char),int64>) =
    let newWorld = Map.fold (processOnePair commands) Map.empty old 
    if counter = 1 then
        newWorld
    else
        nextWorld commands (counter-1) newWorld

let calcResults (old:Map<(char * char),int64>) =
    old
    |> Map.toList
    |> List.map (fun ((l,_),v) -> l,v)
    |> List.groupBy (fun (l,v)->l)
    |> List.map (fun (k,lst)-> 
        k, 
        lst
        |> List.map (fun (_,s) -> s)
        |> List.sum
        )

let calcMinMax str results =
    let (kmax,imax) = List.maxBy (fun (k,i)->i) results
    let (kmin,imin) = List.minBy (fun (k,i)->i) results
    let theMax = 
        if kmax = Seq.last str then
            imax+1L
        else
            imax
    let theMin =
        if kmin = Seq.last str then
            imin+1L
        else
            imin
    let outp = theMax - theMin
    outp

