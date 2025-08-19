module Day2
open System

let rec Add (arr : int[]) i = 
    arr.[arr.[i+3]] <- arr.[arr.[i+1]] + arr.[arr.[i+2]]
    Process arr (i + 4)

and Mult (arr : int[]) i = 
    arr.[arr.[i+3]] <- arr.[arr.[i+1]] * arr.[arr.[i+2]]
    Process arr (i + 4)

and Process (x : int[]) i =
    match x.[i] with
        | 1 -> Add x i
        | 2 -> Mult x i
        | _ -> x.[0]

let firstPart x =
    let program = x |> Array.ofSeq
    program.[1] <- 12
    program.[2] <- 2
    Process program 0

let rec secondPart arr n v prev =
    let output =  
        let p = arr |> Array.ofSeq
        p.[1] <- n
        p.[2] <- v
        Process p 0
    match output < 19690720 with
    | true -> secondPart arr (n + 1) v output
    | false -> 
        match v = 0 with
        | true -> (output, n - 1, 19690720 - prev)
        | false -> (output, n, v)

let input = 
    IO.File.ReadAllText("day02/p.in").Split(',')
    |> Seq.map int

let part1 = firstPart input
let part2 = 
    let (_, n, v) = secondPart input 0 0 0
    100 * n + v

