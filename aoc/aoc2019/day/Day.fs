module Day
open System.IO
open System

type Day() =
    static member Run() =
        use file = new StreamReader("day/p.in")

        let rec readLines() =
            match file.ReadLine() with
            | null -> ()
            | ln ->
                Console.WriteLine(ln)
                readLines()

        readLines()

