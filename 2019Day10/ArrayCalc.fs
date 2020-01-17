module ArrayCalc

open GridForm

let parseInput (s:string) =
    let arr=s.Split [|'\n'|]
    [|for x=0 to (arr.[0].Trim()).Length-1 do
        [|
        for y=0 to arr.Length-1 do
            if arr.[y].[x] = '#' then
                yield true
            else
                yield false
        |]
    |]


let displayInput (b:bool[][]) =
    for x=0 to b.[0].Length-1 do
        for y=0 to b.Length-1 do
            if (b.[x].[y]) then
                SmallBall mf 10.0 0.0 0.0 System.Drawing.Color.Blue (float x) (float -y)
let ex1 = @".#..#
.....
#####
....#
...##"
let ex2 = @"......#.#.
#..#.#....
..#######.
.#.#.###..
.#..#.....
..#....#.#
#..#....#.
.##.#..###
##...#..#.
.#....####"
let ex3 = @".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##"
ex1 |> parseInput |> displayInput

let angle (sx,sy) (ax,ay) =
    System.Math.Round(System.Math.Atan2(float (ax-sx),float (sy-ay)),7)

let hidings (sx,sy) (b:bool[][]) =
    printfn "(%A, %A)" sx sy
    let s=
        seq{
            for x=0 to b.[0].Length-1 do
                for y=0 to b.Length-1 do
                    if (b.[x].[y]) && not (sx=x && sy=y) then
                        let dist=(x-sx)*(x-sx) + (sy-y)*(sy-y)
                        printfn "    (%A, %A) d:%A a:%A" x y dist (angle (sx,sy) (x,y))
                        yield (x, y , dist, angle (sx,sy) (x,y))
        } 
    query {
        for (x, y, dist, angle) in s do
            groupBy angle into g
            select (g)
    }
    |> Seq.map (fun grp->
        grp
        |> Seq.mapi (fun i (x, y, dist, angle)-> 
            printfn "    (%A, %A) d:%A a:%A i:%A" x y dist angle i
            (x, y, dist, angle, i)
            )
        )
    |> Seq.collect (fun elm->elm)
let numVisible (sx,sy) (b:bool[][]) =
    hidings (sx,sy) (b:bool[][])
    |> Seq.filter (fun (_,_,_,_,i)->i=0)
    |> Seq.length

let mapit (b:bool[][]) =
    seq{
        for x=0 to b.[0].Length-1 do
            for y=0 to b.Length-1 do
                if b.[x].[y] then
                    yield x,y, (numVisible (x,y) b)
    }
    |> Seq.sortBy(fun (x,y,n)->n)

//ex3 |> parseInput |> mapit |> Seq.iter (fun (x,y,n)->printfn "(%A, %A) %A" x y n)
//Input.Official|> parseInput |> mapit |> Seq.iter (fun (x,y,n)->printfn "(%A, %A) %A" x y n)
//ex2 |> parseInput |> mapit |> Seq.iter (fun (x,y,n)->printfn "(%A, %A) %A" x y n)
//ex1 |> parseInput |> mapit |> Seq.iter (fun (x,y,n)->printfn "(%A, %A) %A" x y n)

