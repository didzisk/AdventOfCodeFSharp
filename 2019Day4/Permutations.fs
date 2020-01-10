module Permutations

let rec permutations list taken = 
  seq { if Set.count taken = List.length list then yield [] else
        for l in list do
          if not (Set.contains l taken) then 
            for perm in permutations list (Set.add l taken)  do
              yield l::perm }

(*
let rec growingPermutations list taken = 
  seq { if Set.count taken = List.length list then yield [] else
        for l in list do
          if not (Set.contains l taken) then 
            for perm in permutations list (Set.add l taken)  do
                match perm with
                | [] -> yield l::perm 
                | x::xs when l<x -> yield l::perm 
                | _ -> ()
      }
      *)

let permToInt =
    List.fold (fun resultSoFar x -> resultSoFar * 10 + x) 0

      //let ascPermutations

let intToPerm i :int list =
    let unfolderAction stateSoFar =
        match stateSoFar with
        | 0 -> None
        | x -> Some(x % 10, x/10)
    List.unfold unfolderAction i
    |> List.rev

let listIsGrowing (L:int list):bool =
    let (ret,_)=L |> List.fold (fun (resultSoFar,prev) x -> (resultSoFar && (prev<=x), x) ) (true,0)
    ret

let listHasRepeat (L:int list):bool =
    let (ret,_)=L |> List.fold (fun (resultSoFar,prev) x -> (resultSoFar || (prev=x), x) ) (false,0)
    ret

let listHasIsolatedPair (L:int list):bool =
    let (ret,_,_,_)=L @ [0] |> List.fold (fun (resultSoFar, prev, prevprev, ppp) x -> (resultSoFar || (prev<>x && prevprev=prev && ppp<>prevprev), x, prev, prevprev) ) (false,0,0,0)
    ret
