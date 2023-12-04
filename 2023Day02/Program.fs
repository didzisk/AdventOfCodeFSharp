open System
open System.Net
open System.Net.Http

[<Literal>]
let inputFile = __SOURCE_DIRECTORY__ + "\input.txt"

let downloadInput () =
    let cookieContainer = new CookieContainer();
    use handler = new HttpClientHandler(CookieContainer = cookieContainer)
    use c = new HttpClient(handler)
    let sess = "53616c7465645f5f4545bae591f3184fdb136bd7829f24293bdfb611920340de631588d174746123fa5775a4009529e169a310a12426caaa5e919e0f997aefcf"
    cookieContainer.Add(new Uri("https://adventofcode.com"), Cookie("session", sess))

    let s = __SOURCE_DIRECTORY__
    let day = Int32.Parse(s.Substring(s.Length-2))
    let url = $"https://adventofcode.com/2023/day/{day}/input"

    async {
        let! bytes = c.GetByteArrayAsync(url) |> Async.AwaitTask
        System.IO.File.WriteAllBytes(inputFile, bytes)
    }
    |> Async.RunSynchronously

downloadInput()

let lines = System.IO.File.ReadLines inputFile

let parseColor color (s:string) =
    let elm = s.Trim().Split(' ')
    if elm[1] = color then
        Int32.Parse(elm[0])
    else
        0
let parseGame (s:string) =
    let arr = s.Split([|','|])
    let blue = arr |> Array.map(parseColor "blue") |> Array.sum
    let red = arr |> Array.map(parseColor "red") |> Array.sum
    let green = arr |> Array.map(parseColor "green") |> Array.sum
    red, green, blue
let gameStepInvalid (red, green, blue) =
    not (red <= 12 && green <= 13 && blue <= 14)
let parseLine (s:string) =
    let arr = s.Split([|':'; ';'|])
    let d = arr[0].Split([|' '|])
    let day = int d[1]
    let parsedGame =
        arr
        |> Array.skip 1
        |> Array.map parseGame
    parsedGame, day
let parseLine1 (s:string) =
    let parsedGame, day = parseLine s
    let dayValid =
        parsedGame
        |> Array.filter gameStepInvalid
        |> Array.isEmpty
    day, dayValid
let solve1 =
    lines
    |> Seq.map parseLine1
    |> Seq.filter snd
    |> Seq.sumBy fst
    |> printfn "Part1: %d"
let parseLine2 s =
    let rgb, _ = parseLine s
    rgb
let calcLine arr =
    let reds =
        arr
        |> Array.map (fun (x,_,_)-> x)
        |> Array.max
    let greens =
        arr
        |> Array.map (fun (_,x,_)-> x)
        |> Array.max
    let blues =
        arr
        |> Array.map (fun (_,_,x)-> x)
        |> Array.max
    reds * greens * blues    
let solve2 =
    lines
    |> Seq.map parseLine2
    |> Seq.sumBy calcLine
    |> printfn "Part2: %d"
    
