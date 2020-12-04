﻿module Day5Calc

open System.IO
open Common

let Lines filename = 
    File.ReadLines filename
    |> Seq.toArray

let passports lines =
    let mutable s = ""
    seq {
        for l in lines do
            if l = "" then
                yield s
                s <- ""
            else
                s <- s+" "+l
        yield s
        }

let createPassport (line:string) =
    let fields=split " " line
    fields
    |> Array.map (fun x -> 
        let elem=split ":" x
        (elem.[0],elem.[1])
        )
    |> Map.ofArray

let containsField (passport:Map<string,string>) s =
    passport |> Map.containsKey s

let passportValid (passport:Map<string,string>) =
    let reqFields = [|"byr"; "iyr"; "eyr"; "hgt"; "hcl"; "ecl"; "pid"|]
    let numFields=
        reqFields
        |> Array.filter 
            (containsField passport)
            
        |> Array.length
    numFields>6

//byr (Birth Year) - four digits; at least 1920 and at most 2002.
let byrValid (passport:Map<string,string>) =
    let s = passport.Item "byr"

    match System.Int32.TryParse s with
    | true, a -> 
        a>=1920 && a<=2003
    | _ -> false

//iyr (Issue Year) - four digits; at least 2010 and at most 2020.
let iyrValid (passport:Map<string,string>) =
    let s = passport.Item "iyr"

    match System.Int32.TryParse s with
    | true, a -> 
        a>=2010 && a<=2020
    | _ -> false

//eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
let eyrValid (passport:Map<string,string>) =
    let s = passport.Item "eyr"

    match System.Int32.TryParse s with
    | true, a -> 
        a>=2020 && a<=2030
    | _ -> false

//hgt (Height) - a number followed by either cm or in:
//If cm, the number must be at least 150 and at most 193.
//If in, the number must be at least 59 and at most 76.
let hgtValid (passport:Map<string,string>) =
    let s = passport.Item "hgt"

    let inc=index "in" s
    if inc > 0 then
        let hin = s.Substring(0,inc)

        match System.Int32.TryParse hin with
        | true, a -> 
            a>=59 && a<=76
        | _ -> false
    else 
        let cmc=index "cm" s
        if cmc>0 then
            let hcm = s.Substring(0,cmc)
            match System.Int32.TryParse hcm with
            | true, a -> 
                a>=150 && a<=193
            | _ -> false
        else
            false
//hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
let hclValid (passport:Map<string,string>)  =
    let s = passport.Item "hcl"

    let hashPos = index "#" s
    if hashPos<>0 then
        false
    else
        let hex=s.Substring(1)
        if hex.Length <> 6 then
            false
        else
            try
                System.Int32.Parse(hex, System.Globalization.NumberStyles.HexNumber)
                |> ignore
                true
            with
            | _ -> false 


//ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
let eclValid (passport:Map<string,string>)  =
    let s = passport.Item "ecl"

    let validCols = ["amb"; "blu"; "brn"; "gry"; "grn"; "hzl"; "oth"]
    validCols
    |> List.contains s

//pid (Passport ID) - a nine-digit number, including leading zeroes.
let pidValid (passport:Map<string,string>)  =
    let s = passport.Item "pid"
    if s.Length=9 then
        try
            System.Int64.Parse(s)
            |> ignore
            true
        with
        | _ -> false 
    else
        false



let passportValid2 (passport:Map<string,string>) =
    passportValid passport
    && byrValid passport
    && iyrValid passport
    && eyrValid passport
    && hgtValid passport
    && hclValid passport
    && eclValid passport
    && pidValid passport

