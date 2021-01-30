open System

type Player =
    { Number: int
      Symbol: string
      Opponents: int list }

let read () = Console.In.ReadLine()

let N = int (Console.In.ReadLine())

let players =
    [ 1 .. N ]
    |> List.map (fun _ -> read ())
    |> List.map (fun token -> token.Split [| ' ' |])
    |> List.map (fun token ->
        { Number = int token.[0]
          Symbol = token.[1]
          Opponents = List.empty })

let battle player1 player2 =
    let winner1 = { player1 with Opponents = player1.Opponents @ [player2.Number] }
    let winner2 = { player2 with Opponents = player2.Opponents @ [player1.Number] }
    match winner1.Symbol, winner2.Symbol with
    | "C", "P" -> winner1
    | "R", "C" -> winner1
    | "P", "R" -> winner1
    | "R", "L" -> winner1
    | "L", "S" -> winner1
    | "S", "C" -> winner1
    | "C", "L" -> winner1
    | "L", "P" -> winner1
    | "P", "S" -> winner1
    | "S", "R" -> winner1
    
    | "P", "C" -> winner2
    | "R", "P" -> winner2
    | "L", "R" -> winner2
    | "S", "L" -> winner2
    | "C", "S" -> winner2
    | "L", "C" -> winner2
    | "P", "L" -> winner2
    | "S", "P" -> winner2
    | "R", "S" -> winner2
    | "C", "R" -> winner2
    
    | _ -> match winner1.Number < winner2.Number with
           | true -> winner1 | false -> winner2

let rec runPool players =
    let playersLeft =
        players
        |> List.chunkBySize 2
        |> List.map (fun playerVs -> battle playerVs.[0] playerVs.[1])
    match playersLeft with
        | [x] -> x
        | _ -> runPool playersLeft
    
let answer = runPool players
printfn "%i" answer.Number

answer.Opponents
|> List.map string
|> String.concat " "
|> printfn "%s"