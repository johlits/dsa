module Day4
open System
open System.IO

type Day4() =
    static member private IsAscending (s: string) =
        s |> Seq.pairwise |> Seq.forall (fun (a, b) -> a <= b)

    static member private HasDouble (s: string) =
        s
        |> Seq.windowed 4
        |> Seq.exists (fun arr -> 
            (arr.[1] = arr.[2] && arr.[0] <> arr[1] && arr[3] <> arr[1]))

    static member private StartDouble (s: string) =
        s[0] = s[1] && s[2] <> s[0]

    static member private EndDouble (s: string) =
        s[s.Length - 1] = s[s.Length - 2] && s[s.Length - 3] <> s[s.Length - 1]

    static member Run() =
        use file = new StreamReader("day04/p.in")
        while not file.EndOfStream do
            let ln = file.ReadLine()
            if not (String.IsNullOrWhiteSpace ln) then
                let parts = ln.Split("-")
                let lo, hi = int parts.[0], int parts.[1]
                let cnt =
                    { lo .. hi }
                    |> Seq.map (fun i -> string i)
                    |> Seq.filter (fun s ->
                        Day4.IsAscending s && (Day4.HasDouble s || Day4.StartDouble s || Day4.EndDouble s))
                    |> Seq.length

                Console.WriteLine(cnt)

