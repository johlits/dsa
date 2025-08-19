module Day1
open System

let input = 
    IO.File.ReadAllLines("day01/p.in")
    |> Seq.map int

let fuel x = x / 3 - 2

let rec totalFuel x = 
    let f = fuel x
    match f > 0 with
    | true -> f + totalFuel(f)
    | false -> 0

let part1 = input |> Seq.sumBy fuel
let part2 = input |> Seq.sumBy totalFuel