module Day14Calc

open System
open System.IO
open Common

let Lines filename = 
    File.ReadLines filename


let xmask (line:string) (maskline:string) =
    let andmasks = maskline.Replace('X', '1')
    let andmask = Convert.ToInt64(andmasks, 2)
    let ormasks = maskline.Replace('X', '0')
    let ormask = Convert.ToInt64(ormasks, 2)
    let orig = int64 line
    let ored = orig ||| ormask 
    let anded = ored &&& andmask
    //printfn ""
    //printfn "morg %s" maskline
    //printfn "line %s" line
    //printfn "line %s" (Convert.ToString(orig, 2))
    //printfn "orm  %s" ormasks
    //printfn "ored %s" (Convert.ToString(ored, 2))
    //printfn "and  %s" andmasks
    //printfn "ande %s" (Convert.ToString(anded, 2))
    //printfn "ande %d" anded
    anded

let show1 filename =
    let lines = Lines filename
    let mutable currentmask = ""
    seq {
        for line in lines do
            if line.StartsWith "mask" then
                currentmask <- (split " =" line).[1]
            else 
                let saddr = (split " =" line).[0]
                let addr = saddr.Substring(4, saddr.Length-5)
                let arg = (split " =" line).[1]
                yield (sprintf $"addr: {addr} arg: {arg} mask:{currentmask}")
           

            }


let processone (mapstate,currentmask) (line:string) =
    if line.StartsWith "mask" then
        mapstate, (split " =" line).[1]
    else 
        let saddr = (split " =" line).[0]
        let addr = int64 (saddr.Substring(4, saddr.Length-5))
        let arg = (split " =" line).[1]
        let newVal = xmask arg currentmask
        (mapstate |> Map.add addr newVal), currentmask

let calc1 filename =
    Lines filename
    |> Seq.fold processone (Map.empty,"")
    |> fst
    |> Map.toArray
    |> Array.sumBy (fun (adr, x) -> x)

let widdenAddr (addr:string) =
    let a = "000000000000000000000000000000000000"
    a.Substring(0,a.Length-addr.Length)+addr

let addrWithMask (addr:string) (mask:string) =
    let baddr =
        int64 addr
        |> asBinary
        |> widdenAddr

    Array.zip (baddr.ToCharArray()) ((widdenAddr mask).ToCharArray())
    |> Array.toList

let rec allmasksinternal (currentlist:char seq)  (addrAndMask:(char*char) list) =
    seq{
        match addrAndMask with
        | []->yield currentlist
        | (a,m)::xs -> 
            if m = '0' then
                let newcurrentlist = 
                    seq{
                        yield! currentlist 
                        yield a
                        }
                yield! allmasksinternal newcurrentlist xs 
            else 
                if m='1' then
                    let newcurrentlist = 
                        seq{
                            yield! currentlist 
                            yield '1'
                            }
                    yield! allmasksinternal newcurrentlist xs
                else 
                    if m='X' then
                        let newcurrentlist1 = 
                            seq{
                                yield! currentlist 
                                yield '0'
                                }
                        yield! allmasksinternal newcurrentlist1 xs
                        let newcurrentlist2 = 
                            seq{
                                yield! currentlist 
                                yield '1'
                                }
                        yield! allmasksinternal newcurrentlist2 xs
                    else
                        ()
    }

let allmasks (addr:string) (mask:string) =
    addrWithMask addr mask
    |> allmasksinternal Seq.empty 
    |> Seq.map charSeqToString 

let allmasksInt (addr:string) (mask:string) =
    allmasks addr mask 
    |> Seq.map (fun x -> Convert.ToInt64((asString x), 2))
    |> Seq.toList

let processtwo ((mapstate:Map<int64,int64>),currentmask:string) (line:string) =
    if line.StartsWith "mask" then
        mapstate, (split " =" line).[1]
    else 
        let saddr = (split " =" line).[0]
        let addr = saddr.Substring(4, saddr.Length-5)
        let addresses = allmasksInt addr currentmask 
        let arg = int64 ((split " =" line).[1])
        let newMapState = 
            addresses
            |> List.fold 
                (fun mapstateSoFar currAddr ->
                    mapstateSoFar
                    |> Map.add currAddr arg 
                )
                mapstate
        newMapState, currentmask

let calc2 filename =
    Lines filename
    |> Seq.fold processtwo (Map.empty,"")
    |> fst
    |> Map.toArray
    |> Array.sumBy (fun (adr, x) -> x)

let show2 filename =
    Lines filename
    |> Seq.fold processtwo (Map.empty,"")
    |> fst
    |> Map.toArray
    |> printfn "%A"