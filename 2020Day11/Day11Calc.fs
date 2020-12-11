module Day11Calc

open System.IO
open Common

let Lines filename = 
    File.ReadLines filename
    |> Seq.toArray

let numNeighbors row col lines : int=
    let rowc = Array.length lines
    let colc = String.length lines.[0]
    let leftn =
        if col>0 then
            if lines.[row].[col-1]='#' then
                1
            else
                0
        else 0
    let lefttop =
        if col>0 && row>0 then
            if lines.[row-1].[col-1]='#' then
                1
            else
                0
        else 0
    let righttop =
        if col<colc-1  && row>0 then
            if lines.[row-1].[col+1]='#' then
                1
            else
                0
        else 0

    let rightn =
        if col<colc-1 then
            if lines.[row].[col+1]='#' then
                1
            else
                0
        else 0

    let topn = 
        if row>0 then
            if lines.[row-1].[col] = '#' then
                1
            else
                0
        else 0
    let downn =
        if row<rowc-1 then
            if lines.[row+1].[col] = '#' then
                1
            else
                0
        else 0

    let leftdown =
        if col>0 && row<rowc-1 then
            if lines.[row+1].[col-1] = '#' then
                1
            else
                0
        else 0

    let rightdown =
        if col<colc-1 && row<rowc-1 then
            if lines.[row+1].[col+1] = '#' then
                1
            else
                0
        else 0

    leftn + rightn + topn + downn + lefttop + righttop + leftdown + rightdown

let nextRound lines =
    lines
    |> Array.mapi 
        (fun row line ->
            line
            |> String.mapi 
                ( fun col char ->
                    if char = '.' then
                        '.'
                    else
                        if char = 'L' && (numNeighbors row col lines) = 0 then
                            '#'
                        else
                            if char = '#' && (numNeighbors row col lines) >= 4 then
                                'L'
                            else
                                char
                )
        )

let arrEqual lines newLines =
    let eq=
        lines
        |> Array.compareWith 
            (fun a b -> 
                if a=b then
                    0
                else
                    1
            )
            newLines
    eq=0

let rec goNext lines =
    let newLines = nextRound lines
    if arrEqual lines newLines then
        newLines
    else
        goNext newLines

let countPassengers lines =
    lines
    |> Array.map 
        ( fun s ->
            s
            |> String.filter (fun x->x='#')
            |> String.length
        )
    |> Array.sum

let outside lines row col =
    let rowc = Array.length lines
    let colc = String.length lines.[0]
    row<0 || col<0 || row>=rowc || col >= colc

type Dir =
    | N
    | NE
    | E
    | SE
    | S
    | SW
    | W
    | NW

 let rec goDir lines row col dir =
    let newrow, newcol =
        match dir with
        | N -> row-1, col
        | NE -> row-1, col+1
        | E -> row, col+1
        | SE -> row+1, col+1
        | S -> row+1, col
        | SW -> row+1, col-1
        | W -> row, col-1
        | NW -> row-1, col-1
    if outside lines newrow newcol || lines.[newrow].[newcol] = 'L' then
        0
    else
        if lines.[newrow].[newcol] = '#' then
            1
        else
            goDir lines newrow newcol dir

let numNeighbors2 row col lines : int=
    let res =
        goDir lines row col Dir.N
        + goDir lines row col Dir.NE
        + goDir lines row col Dir.E
        + goDir lines row col Dir.SE
        + goDir lines row col Dir.S
        + goDir lines row col Dir.SW
        + goDir lines row col Dir.W
        + goDir lines row col Dir.NW
    res

let nextRound2 lines =
    lines
    |> Array.mapi 
        (fun row line ->
            line
            |> String.mapi 
                ( fun col char ->
                    if char = '.' then
                        '.'
                    else
                        let neighbors2 = numNeighbors2 row col lines
                        if char = 'L' && neighbors2 = 0 then
                            '#'
                        else
                            if char = '#' && neighbors2 >= 5 then
                                'L'
                            else
                                char
                )
        )

let rec goNext2 lines =
    let newLines = nextRound2 lines
    if arrEqual lines newLines then
        newLines
    else
        goNext2 newLines
