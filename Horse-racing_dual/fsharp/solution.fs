open System

let read x = Console.In.ReadLine()

let N = read 0 |> int

[0..N-1]
|> List.map read
|> List.map int
|> List.sortDescending
|> List.pairwise
|> List.map (fun tuple -> tuple ||> (-))
|> List.min
|> printfn "%i"