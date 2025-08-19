module Day3
open System.IO
open System
open System.Collections.Generic

type Day3() =
    static member Run() =
        use file = new StreamReader("day03/p.in")

        let rec parseLine (lineNo: int) (l1: (char * int) list) (l2: (char * int) list) =
            match file.ReadLine() with
            | null -> (l1, l2)
            | ln ->
                let parts = ln.Split(',') |> Array.toList
                let dirLengthTuple (part: string) : char * int =
                    (part.[0], int32 (part.Substring(1)))
                let l1', l2' =
                    if lineNo = 0 then
                        List.fold (fun acc part -> dirLengthTuple part :: acc) l1 parts |> List.rev, l2
                    else
                        l1, List.fold (fun acc part -> dirLengthTuple part :: acc) l2 parts |> List.rev
                parseLine (lineNo + 1) l1' l2'

        let l1, l2 = parseLine 0 [] []

        let mutable closest = Int32.MaxValue
        let visited = HashSet<Tuple<int, int>>()
        let dic = Dictionary<Tuple<int, int>, int>()

        let mutable current = (0, 0)
        let mutable steps = 1
        for i = 0 to List.length l1 - 1 do
            let dir, len = List.item i l1
            for _ = 0 to len - 1 do
                match dir with
                | 'U' -> current <- (fst current, snd current - 1)
                | 'D' -> current <- (fst current, snd current + 1)
                | 'L' -> current <- (fst current - 1, snd current)
                | 'R' -> current <- (fst current + 1, snd current)
                visited.Add(current)
                if not (dic.ContainsKey current) then
                    dic.Add(current, steps)
                steps <- steps + 1

        current <- (0, 0)
        steps <- 1
        for i = 0 to List.length l2 - 1 do
            let dir, len = List.item i l2
            for _ = 0 to len - 1 do
                match dir with
                | 'U' -> current <- (fst current, snd current - 1)
                | 'D' -> current <- (fst current, snd current + 1)
                | 'L' -> current <- (fst current - 1, snd current)
                | 'R' -> current <- (fst current + 1, snd current)
                if visited.Contains current then
                    if steps + dic.[current] < closest then
                        closest <- steps + dic.[current]
                steps <- steps + 1

        Console.WriteLine(closest)
        file.Close()

