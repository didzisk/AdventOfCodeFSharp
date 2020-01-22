module DictCalc

open System
open GridForm

let parseInput (s:string) =
    let arr=s.Split [|'\n'|]
    seq {
        for x=0 to (arr.[0].Trim()).Length-1 do
            for y=0 to arr.Length-1 do
                if arr.[y].[x] = '#' then
                    yield (x,y)
    }

    
let displayInput (m:seq<(int * int)>) =
    m|> Seq.iter (fun (x,y) -> 
        SmallBall mf 10.0 0.0 0.0 System.Drawing.Color.Green (float x) (float -y))

let displayInputWithStation (sx,sy) (m:seq<(int * int)>)  =
    m|> Seq.iter (fun (x,y) -> 
        SmallBall mf 10.0 0.0 0.0 System.Drawing.Color.Green (float x) (float -y))
    SmallBall mf 10.0 0.0 0.0 System.Drawing.Color.Red (float sx) (float -sy)

let angle1 (sx,sy) (ax,ay) =
    Math.Atan2(float (sx-ax),float (ay-sy))
    |> fun z->Math.Round (z,8)

let angle2 (sx,sy) (ax,ay) =
    
    let x=float (ax-sx)
    let y=float (sy-ay)
    if x >= 0.0 then
        if y>0.0 then
            x/y
        else
            1.0-y/x
    else
        if y<0.0 then
            2.0+x/y
        else
            3.0-y/x
    |> fun z->Math.Round (z,8)

let angle3 (sx,sy) (ax,ay) =
    let x=float (ax-sx)
    let y=float (sy-ay)
    let len=Math.Sqrt(x*x+y*y)
    if x >= 0.0 then
        if y>=0.0 then
            x/len
        else
            1.0-y/len
    else
        if y<0.0 then
            2.0-x/len
        else
            3.0+y/len
    |> fun z->Math.Round (z,8)

let angle4 (sx,sy) (ax,ay) =
    let x=float (ax-sx)
    let y=float (sy-ay)
    let len=x*x+y*y
    if x >= 0.0 then
        if y>=0.0 then
            x*x/len
        else
            1.0+y*y/len
    else
        if y<0.0 then
            2.0+x*x/len
        else
            3.0+y*y/len
    |> fun z->Math.Round (z,8)

let angle = angle2

let hidings (sx,sy) (m:seq<(int * int)>) =
    let s=
        m 
        |> Seq.filter(fun (x,y)->sx<>x || sy<>y)
        |> Seq.map (fun (x,y)->
            let dist=(x-sx)*(x-sx) + (sy-y)*(sy-y)
            (x, y , dist, angle (sx,sy) (x,y))
            )
    query {
        for (y, x, dist, angle) in s do
            sortBy dist
            groupBy angle into g
            select (g)
    }
    |> Seq.map (fun grp->
        grp
        |> Seq.mapi (fun i (x, y, dist, angle)-> 
            (x, y, dist, angle, i)
            )
        )
    |> Seq.collect (fun elm->elm)

let numVisible (sx,sy) m =
    hidings (sx,sy) m
    |> Seq.filter (fun (_,_,_,_,i)->i=0)
    |> Seq.length

let mapit1 m =
    m 
    |> Seq.map (fun (x,y)->(x,y, (numVisible (x,y) m)))
    |> Seq.sortBy(fun (x,y,n)->n)

let bestPosSimple m =
    m 
    |> Seq.last

let bestPosPrint s =
    s |> parseInput |> mapit1 |> bestPosSimple

let mapit2 (sx,sy) m =
    hidings (sx,sy) m 
    |> Seq.sortBy(fun (x,y,dist,angle,n)->float n*100.0+angle)

let the200th s=
    let m=parseInput s
    let (sx,sy,_)=bestPosPrint s
    mapit2 (sx,sy) m
    |> Seq.mapi (fun i (x,y,d,a,n)->(i+1,x,y,d,a,n))

let MainCalc = 
    Input.ex3 |> parseInput |> displayInput
//    Input.ex1 |> parseInput |> mapit1 |> Seq.iter (fun elm->printf "%A" elm)
//    Input.Official |> parseInput |> mapit1 |> Seq.iter (fun elm->printf "%A" elm)

    Input.Official |> bestPosPrint |> printf "%A"

    Input.ex3 |> bestPosPrint |> printf "%A"
    let (sx,sy,_)=Input.ex3 |> bestPosPrint
    Input.ex3 |> parseInput |> displayInputWithStation (sx, sy)

    //Input.ex3 |> parseInput |> hidings (sx,sy)|> Seq.iter (printfn "%A")

    Input.ex3 |> the200th |> Seq.iter (printfn "%A")

    Input.Official |> the200th |> Seq.iter (printfn "%A")