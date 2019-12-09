    let lev x tpow =
        let l=x/tpow
        let k=l*tpow
        let r=x-k
        (l, r)


    let isGrowing(d1, d2, d3, d4, d5, d6) =
        (d6<=d5) && (d5<=d4) && (d4<=d3) && (d3<=d2)&& (d2<=d1)

    let chk predicate x=
        let (d6, r6) =lev x  100000
        let (d5, r5) =lev r6 10000
        let (d4, r4) =lev r5 1000
        let (d3, r3) =lev r4 100
        let (d2, d1) =lev r3 10

        let x = (d1, d2, d3, d4, d5, d6)
        isGrowing x && predicate x

    let hasPair(d1, d2, d3, d4, d5, d6) =
        (d1=d2) ||
        (d2=d3) ||
        (d3=d4) ||
        (d4=d5) ||
        (d5=d6)

    let hasIsolatedPair (d1, d2, d3, d4, d5, d6) =
        (d1=d2 && d2<>d3) ||    
        (d1<>d2 && d2=d3 && d3<>d4) ||
        (d2<>d3 && d3=d4 && d4<>d5) ||
        (d3<>d4 && d4=d5 && d5<>d6) ||
        (d4<>d5 && d5=d6)

    let solve predicate a=
        a
        |> List.filter(chk predicate)
        |> List.length

    let input = [ 264793..803935 ]
    let part1 = solve hasPair input
    let part2 = solve hasIsolatedPair input