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

let getTieBattleWinner player1 player2 =
    if player1.Number < player2.Number then player1 else player2

let battle player1 player2 =
    let winner1 = { player1 with Opponents = player1.Opponents @ [player2.Number] }
    let winner2 = { player2 with Opponents = player2.Opponents @ [player1.Number] }
    match winner1.Symbol, winner2.Symbol with
    | "C", "P" -> winner1
    | "P", "C" -> winner2
    | "P", "R" -> winner1
    | "R", "P" -> winner2
    | "R", "L" -> winner1
    | "L", "R" -> winner2
    | "L", "S" -> winner1
    | "S", "L" -> winner2
    | "S", "C" -> winner1
    | "C", "S" -> winner2
    | "C", "L" -> winner1
    | "L", "C" -> winner2
    | "L", "P" -> winner1
    | "P", "L" -> winner2
    | "P", "S" -> winner1
    | "S", "P" -> winner2
    | "S", "R" -> winner1
    | "R", "S" -> winner2
    | "R", "C" -> winner1
    | "C", "R" -> winner2
    | s1, s2 when s1 = s2 ->
        getTieBattleWinner winner1 winner2
    | _ ->
        Console.Out.WriteLine "Battle pattern not found"
        winner1


let splitList list = List.foldBack (fun x (l, r) -> x :: r, l) list ([], [])

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