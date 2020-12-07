module Day7Calc

open System.IO
open Common
open System.Text.RegularExpressions

let Lines filename = 
    File.ReadLines filename
    |> Seq.toArray

//vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
//faded blue bags contain no other bags.


//for line in lines:
//color = re.match(r'(.+?) bags contain', line)[1]
//for ct, innercolor in re.findall(r'(\d+) (.+?) bags?[,.]', line):
//    ct = int(ct)
//    containedin[innercolor].add(color)
//    contains[color].append((ct, innercolor))
(*
let addToContainedIn (containedIn:Map<string, Set<string>>) (line:string)
    let thisContainedIn =
        if containedIn.ContainsKey innercolor then
            Set.add thisColor (containedIn.Item innercolor)
        else
            Set.add thisColor Set.empty
    

let parseOneLine (line:string) (containedIn:Map<string, Set<string>>) (contains:Map<string, List<(int * string)>>) =
    
    let thisColor=Regex.Match( line, "(.+?) bags contain").Value
    for m in Regex.Matches(line, "(\d+) (.+?) bags?[,.]") do
        let contentsEntry = split " " (m.Value)
        let contentsCount = int32 contentsEntry.[0]
        let innercolor = contentsEntry.[1]
        let thisContainedIn =
            if containedIn.ContainsKey innercolor then
                Set.add thisColor (containedIn.Item innercolor)
            else
                Set.add thisColor Set.empty
    let newcontainedIn = Map.add thisColor thisContainedIn containedIn
    Set.add innercolor thisContainedIn


let containedIn lines =
    let thisContainedIn = Set.empty<string>
    for line in lines do
        let m = Regex.Match( line, "(.+?) bags contain")
        let contentsEntry = split " " (Regex.Matches(line, "(\d+) (.+?) bags?[,.]"))
        let contentsCount = int32 contentsEntry.[0]
        let innercolor = contentsEntry.[1]
        
        Set.add innercolor thisContainedIn
        ()
    thisContainedIn

    *)