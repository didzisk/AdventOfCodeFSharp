module Day20Calc

open System
open System.IO
open Common

let calcToplineCode (pic:bool[,]) : int =
    pic.[0,*]
    |> Array.mapi 
        (fun col b-> 
            if b then 
                let mask = 1 <<< col
                mask
            else 0
        )
    |> Array.sum

let transpose (pic:bool[,]) : bool[,] =
    let len = Array2D.length1 pic
    let res = Array2D.zeroCreate len len
    for row = 0 to len-1 do
        for col = 0 to len-1 do
            Array2D.set res col row (pic.[row, col])
    res

let flip (pic:bool[,]) : bool[,] =
    let len = Array2D.length1 pic
    let res = Array2D.zeroCreate len len
    for row = 0 to len-1 do
        for col = 0 to len-1 do
            Array2D.set res row col (pic.[row, len-col-1])
    res

let rotateRight(pic:bool[,]) : bool[,] =
    let len = Array2D.length1 pic
    let res = Array2D.zeroCreate len len
    for row = 0 to len-1 do
        for col = 0 to len-1 do
            Array2D.set res row col (pic.[len-col-1, row])
    res

let perform (op:int) (pic:bool[,]) : bool[,] =
    match op with
    | 0 -> pic
    | 1-> (rotateRight pic)
    | 2-> rotateRight (rotateRight pic)
    | 3-> rotateRight (rotateRight (rotateRight pic))
    | 4-> flip pic
    | 5-> rotateRight (flip pic)
    | 6-> rotateRight (rotateRight (flip pic))
    | _->rotateRight (rotateRight (rotateRight (flip pic)))

type Tile = {pic:bool[,]; edges:int[]; x:int; y:int; op:int; title:int64; used:bool}

let setNewPic (tiles:Tile[]) (arrayIndex:int) (tileTitle:int64) (pic:bool[,])=
    let edges = Array.zeroCreate 8
    edges.[0] <- perform 0 pic |> calcToplineCode //top
    edges.[1] <- perform 1 pic |> calcToplineCode //left
    edges.[2] <- perform 2 pic |> calcToplineCode //bottom, flipped
    edges.[3] <- perform 3 pic |> calcToplineCode //right, flipped
    edges.[4] <- perform 4 pic |> calcToplineCode //top, flipped
    edges.[5] <- perform 5 pic |> calcToplineCode //right
    edges.[6] <- perform 6 pic |> calcToplineCode //bottom
    edges.[7] <- perform 7 pic |> calcToplineCode //left, flipped
    tiles.[arrayIndex] <- {pic = pic; edges = edges; x=0; y=0; op=0; title = tileTitle; used = false}


let readTiles filename =
    let lines = File.ReadAllLines filename
    let numTiles = (Array.length lines) /12 + 1
    let tiles = Array.zeroCreate numTiles
    for i = 0 to (numTiles-1) do
        let startLine = i * 12
        let tileTitle = int64 (lines.[startLine].Substring(5,4))
        let pic = Array2D.zeroCreate 10 10
        for row = 0 to 9 do
            let line = lines.[startLine+1+row]
            for col = 0 to 9 do
                if line.[col] = '#' then
                    Array2D.set pic row col true
                else
                    ()
        setNewPic tiles i tileTitle pic
    tiles

let showPic (pic:bool[,]) =
    let len = Array2D.length1 pic
    for row = 0 to len-1 do
        printfn ""
        for col = 0 to len-1 do
            if pic.[row,col] then
                printf "#"
            else
                printf "."

let showTile (tile:Tile) =
    showPic tile.pic
    printfn ""
    tile.edges
    |> Array.map (printfn "%d")
    
let goLeft ((tiles:Tile array),(idx:int)) =
    let tile = tiles.[idx]
    printfn ""
    //showPic tile.pic
    printfn "going left %d" idx
    let candidate =
        tiles
        |> Array.findIndex
            (fun b-> 
                b.title <> tile.title &&
                b.edges
                |> Array.exists (fun x -> x=tile.edges.[1])
            )
    let c = tiles.[candidate]
    let op = 
        c.edges
        |> Array.findIndex (fun x-> x=tile.edges.[1])
    printfn "%d %d" tile.edges.[3] c.edges.[op]
    let newPic = 
        (perform op c.pic)
        |> perform 3
        |> perform 4

    setNewPic tiles candidate c.title newPic

    //showPic newPic
    (tiles,candidate)

let goRight ((tiles:Tile array),(idx:int)) =
    let tile = tiles.[idx]
    //showTile tile
    printfn "going right %d" idx
    let candidate =
        tiles
        |> Array.findIndex
            (fun b-> 
                b.title <> tile.title &&
                b.edges
                |> Array.exists (fun x -> x=tile.edges.[5])
            )
    let c = tiles.[candidate]
    let op = 
        c.edges
        |> Array.findIndex (fun x-> x=tile.edges.[5])
//    printfn "%d %d" tile.edges.[5] c.edges.[op]
    let newPic = perform 3 (perform op c.pic)

    setNewPic tiles candidate c.title newPic

//    showPic newPic
    (tiles,candidate)

let goUp ((tiles:Tile array),(idx:int)) =
    let tile = tiles.[idx]
    //showTile tile
    printfn "going up %d" idx
    let candidate =
        tiles
        |> Array.findIndex
            (fun b-> 
                b.title <> tile.title &&
                b.edges
                |> Array.exists (fun x -> x=tile.edges.[0])
            )
    let c = tiles.[candidate]
    let op = 
        c.edges
        |> Array.findIndex (fun x-> x=tile.edges.[0])
//    printfn "%d %d" tile.edges.[3] c.edges.[op]
    let newPic = 
        (perform op c.pic)
        |> perform 2
        |> perform 4

    setNewPic tiles candidate c.title newPic

    //showPic newPic
    (tiles,candidate)

let goDown ((tiles:Tile array),(idx:int)) =
    let tile = tiles.[idx]
    //showTile tile
    printfn "going down %d" idx
    let candidate =
        tiles
        |> Array.findIndex
            (fun b-> 
                b.title <> tile.title &&
                b.edges
                |> Array.exists (fun x -> x=tile.edges.[6])
            )
    let c = tiles.[candidate]
    let op = 
        c.edges
        |> Array.findIndex (fun x-> x=tile.edges.[6])
//    printfn "%d %d" tile.edges.[3] c.edges.[op]
    let newPic = 
        (perform op c.pic)

    setNewPic tiles candidate c.title newPic

    //showPic newPic
    (tiles,candidate)

let removeBorders (pic:bool[,]) =
    pic.[1..8, 1..8]

let calc1 filename =
    
    let tiles = 
        filename
        |> readTiles

    let rightEdge=
        goRight (tiles,0)
        |> goRight
        |> goRight
        |> goRight
        |> goRight
        |> goRight

    let leftEdge =
        goLeft (tiles,0)
        |> goLeft 
        |> goLeft 
        |> goLeft 
        |> goLeft 

    let topLeft = 
        leftEdge
        |> goUp
        |> goUp
        |> goUp
        |> goUp
        |> goUp

    let topRight = 
        rightEdge
        |> goUp
        |> goUp
        |> goUp
        |> goUp
        |> goUp

    let bottomLeft = 
        leftEdge
        |> goDown
        |> goDown
        |> goDown
        |> goDown
        |> goDown
        |> goDown

    let bottomRight =
        rightEdge
        |> goDown
        |> goDown
        |> goDown
        |> goDown
        |> goDown
        |> goDown

    let tl = (snd topLeft |> (Array.get tiles)).title
    let tr = (snd topRight|> (Array.get tiles)).title
    let bl = (snd bottomLeft|> (Array.get tiles)).title
    let br = (snd bottomRight|> (Array.get tiles)).title

    printfn "Part1: %d" (tl*tr*bl*br)

let addOneTile ((arr:bool[,]),(row:int),(col:int),((tiles:Tile array),(idx:int))) =
    let tile = tiles.[idx]
    tiles.[idx] <- {tile with used = true}
    let pic = removeBorders tile.pic
    for r = 0 to 7 do
        for c = 0 to 7 do
            arr.[row*8+r, col*8+c] <- pic.[r,c]
    printfn "%d,%d" row col 
    (arr, row, col, (tiles,idx))

let addWithMove ((arr:bool[,]),(row:int),(col:int),((tiles:Tile array),(idx:int))) =
    let (narr, nrow, ncol, (ntiles,nidx)) = addOneTile (arr, row, col, (tiles,idx))
    let (newTiles, newIdx) =
         goRight (tiles, idx)
    (narr, nrow, ncol+1, (newTiles,newIdx))

let addOneLine ((arr:bool[,]),(row:int),(col:int),((tiles:Tile array),(idx:int))) =
    (arr, row, col, (tiles,idx))
        |> addWithMove
        |> addWithMove
        |> addWithMove
        |> addWithMove
        |> addWithMove
        |> addWithMove
        |> addWithMove
        |> addWithMove
        |> addWithMove
        |> addWithMove
        |> addWithMove
        |> addOneTile


let addLineWithMove ((arr:bool[,]),(row:int),(col:int),((tiles:Tile array),(idx:int))) =
    let (narr, nrow, ncol, (ntiles,nidx)) = addOneLine (arr, row, col, (tiles,idx))
    let (newTiles, newIdx) = goDown (tiles, idx)
    (narr, nrow+1, 0, (newTiles,newIdx))

let calc2arr filename =
    
    let tiles = 
        filename
        |> readTiles

    let leftEdge =
        goLeft (tiles,0)
        |> goLeft 
        |> goLeft 
        |> goLeft 
        |> goLeft 

    let topLeft = 
        leftEdge
        |> goUp
        |> goUp
        |> goUp
        |> goUp
        |> goUp

    let arr = Array2D.zeroCreate 96 96

    let (narr, nrow, ncol, (ntiles,nidx)) = 
        (arr, 0, 0, topLeft)
        |> addLineWithMove //1
        |> addLineWithMove//2
        |> addLineWithMove//3
        |> addLineWithMove//4
        |> addLineWithMove//5
        |> addLineWithMove//6
        |> addLineWithMove//7

        |> addLineWithMove//8
        |> addLineWithMove//9
        |> addLineWithMove//10
        |> addLineWithMove//11
        |> addOneLine
        
    narr
          
let readWorm = 
    let lines = File.ReadAllLines "Day20Worm.txt"
    seq {
        for row = 0 to (Array.length lines-1) do
            for col = 0 to (String.length lines.[row] - 1) do
                if lines.[row].[col] = '#' then
                    yield row, col
         }
    |> Seq.toList

let isWorm (arr:bool[,]) (row:int) (col:int) (worm:(int*int) list) =
    let len = Array2D.length1 arr
    worm
    |> List.exists
        (fun (rw, cw) ->
            if row + rw >= len || col + cw >= len then
                true //it snakes out of bounds
            else
                arr.[row+rw, col+cw] = false //it's empty where worm is supposed to be
        )
    |> not

let findWorms arr =
    let worm = readWorm
    let len = Array2D.length1 arr
    seq {
        for row = 0 to len-1 do
            for col = 0 to len-1 do
                if isWorm arr row col worm then
                    yield row,col
                else
                    ()
    }

let countWorms arr =
    findWorms arr
    |> Seq.length

let countWormsAll arr =
    for op = 0 to 7 do
        printfn "%d %d" op (countWorms (perform op arr))

let calc2count filename = 
    let arr = calc2arr filename 
    countWormsAll arr

let calcFree2 filename =
    let arr = calc2arr filename 
    let numBlack = 
        let len = Array2D.length1 arr
        seq {
            for row = 0 to len-1 do
                for col = 0 to len-1 do
                    if arr.[row,col] then
                        yield true
                    else
                        ()
        }
        |> Seq.length
    let numInWorms = 26* 15
    numBlack - numInWorms
    
    

(*
let drawWorms filename =
    let arr =
        calc2arr filename
        |> perform 2

    let worm = readWorm
    let worms = findWorms arr

    for (row,col) in worms do
        
        *)