open System

type Player =
    { Number: int
      Symbol: string }

let read () = Console.In.ReadLine()

let testInputData = [ "8"; "4 R"; "1 P"; "8 P"; "3 R"; "7 C"; "5 S"; "6 L"; "2 L" ]
let N = testInputData.[0] |> int

let players =
    [ 1 .. N ]
    //            |> List.map read
    //            |> List.map (fun token -> token.Split [| ' ' |])
    |> List.map (fun index -> testInputData.[index].Split [| ' ' |])
    |> List.map (fun token ->
        { Number = int token.[0]
          Symbol = token.[1] })

//let N = int (Console.In.ReadLine())

let getTieBattleWinner player1 player2 =
    if player1.Number < player2.Number then player1 else player2

let battle player1 player2 =
    match player1.Symbol, player2.Symbol with
    | "C", "P" -> player1
    | "P", "C" -> player2
    | "P", "R" -> player1
    | "R", "P" -> player2
    | "R", "L" -> player1
    | "L", "R" -> player2
    | "L", "S" -> player1
    | "S", "L" -> player2
    | "S", "C" -> player1
    | "C", "S" -> player2
    | "C", "L" -> player1
    | "L", "C" -> player2
    | "L", "P" -> player1
    | "P", "L" -> player2
    | "P", "S" -> player1
    | "S", "P" -> player2
    | "S", "R" -> player1
    | "R", "S" -> player2
    | "R", "C" -> player1
    | "C", "R" -> player2
    | s1, s2 when s1 = s2 ->
        getTieBattleWinner player1 player2
    | _ ->
        Console.Out.WriteLine "Battle pattern not found"
        player1


let splitList list = List.foldBack (fun x (l, r) -> x :: r, l) list ([], [])

let rec runPool players =
    let playersLeft =
        players
        |> List.chunkBySize 2
        |> List.map (fun playerVs -> battle playerVs.[0] playerVs.[1])
    match List.length playersLeft with
        | 1 -> playersLeft.[0]
        | _ -> runPool playersLeft
    



let answer = runPool players
Console.Out.WriteLine answer.Number
