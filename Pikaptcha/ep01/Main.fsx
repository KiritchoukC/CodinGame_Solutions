(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

// test data
//let width = 5
//let height = 3
//let lines = ["0000#"
//             "#0#00"
//             "00#0#"]
//let grid =
//    [ 0 .. height - 1 ]
//    |> List.map (fun x -> lines.[x])
//    |> List.map Seq.toList
    

let read () = Console.In.ReadLine()

let token = (read ()).Split [| ' ' |]
let width = int (token.[0])
let height = int (token.[1])
let grid =
    [ 0 .. height - 1 ]
    |> List.map (fun x -> read ())
    |> List.map Seq.toList

let directions =
    [ (-1, 0)
      (0, 1)
      (1, 0)
      (0, -1) ]

let isNotWall item =
    match item with
    |'#' -> None
    | _ -> Some item

let getCell x y =
    grid
    |> List.tryItem y
    |> Option.bind (List.tryItem x)
    |> Option.bind isNotWall

let getWay x y =
    match (x, y) with
    | (-1, _) -> None
    | (_, -1) -> None
    | (x, _) when x >= width -> None
    | (_, y) when y >= height -> None
    | (x, y) -> getCell x y
    | (_, _) -> None

let findWays x y =
    directions
    |> List.map (fun direction -> getWay (x + fst direction) (y + snd direction))
    |> List.filter Option.isSome
    |> List.length
    |> string
    |> char

let findWaysIfNotWall x y char =
    match char with
    | '0' -> findWays x y
    | _ -> '#'

let charListToString charList =
    charList
    |> List.map string
    |> List.reduce (+)

grid
|> List.mapi (fun y line -> List.mapi (fun x c -> findWaysIfNotWall x y c) line)
|> List.map charListToString
|> List.iter (fun line -> printfn "%s" line)
