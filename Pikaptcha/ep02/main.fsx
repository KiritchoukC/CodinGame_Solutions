module Array2D =
    let find item (arr: 'a [,]) =
        let rec go x y =
            if y >= arr.GetLength 1 then None
            elif x >= arr.GetLength 0 then go 0 (y + 1)
            elif arr.[x, y] = item then Some(x, y)
            else go (x + 1) y
        go 0 0

open System

type Side =
    | Left
    | Right

let (width, height) =
    (Console.In.ReadLine()).Split [| ' ' |]
    |> Array.map int
    |> fun c -> (c.[0], c.[1])

let lines = [ 0 .. height - 1 ] |> List.map (fun c -> Console.In.ReadLine())

let side =
    match Console.In.ReadLine() with
    | "R" -> Right
    | "L" -> Left
    | _ -> failwith "Wrong side input"

let maze0 = Array2D.init height width (fun y x -> lines.[y].[x])

let (x0, y0, orientation0) =
    [ '>'; '<'; '^'; 'v' ]
    |> List.map (fun o ->
        let source = Array2D.find o maze0
        match source with
        | None -> None
        | Some coords -> Some(snd coords, fst coords, o))
    |> List.filter Option.isSome
    |> List.map Option.get
    |> List.head

let getCell x y maze = 
    try
        Some (Array2D.get maze y x)
    with
    |  :? IndexOutOfRangeException -> None

let incrementCell x y maze =
    let mazeCopy = Array2D.copy maze
    let current = getCell x y mazeCopy

    match current with
    | None -> mazeCopy
    | Some c ->
        match c with
        | '#' -> mazeCopy
        | '>' | '<' | '^' | 'v' ->
            Array2D.set mazeCopy y x '1'
            mazeCopy
        | c ->
            let incremented =
                c
                |> string
                |> int
                |> (+) 1
                |> string
                |> Seq.head
            Array2D.set mazeCopy y x incremented
            mazeCopy


let go x y maze =
    let isOut = x >= width || y >= height || x < 0 || y < 0
    match isOut with
    | true -> None
    | false ->
        let nextCell = getCell x y maze
        match nextCell with
        | None -> None
        | Some cell ->
            let isDone =
                not (cell = '0') && (x = x0 && y = y0)
            match isDone with
            | true -> Some(0, 0, maze, true)
            | false ->
                match cell with
                | '#' -> None
                | _ ->
                    let updatedMaze = incrementCell x y maze
                    Some(x, y, updatedMaze, false)

let goRight x y = go (x + 1) y
let goLeft x y = go (x - 1) y
let goDown x y = go x (y + 1)
let goUp x y = go x (y - 1)

let rec goOn orientation x y maze =
    match side with
    | Left -> 
        match orientation with
        | '>' ->
            match goUp x y maze with
            | Some (tx, ty, tMaze, isDone) -> (tx, ty, '^', tMaze, isDone)
            | None -> goOn 'v' x y maze
        | 'v' ->
            match goRight x y maze with
            | Some (tx, ty, tMaze, isDone) -> (tx, ty, '>', tMaze, isDone)
            | None -> goOn '<' x y maze
        | '<' ->
            match goDown x y maze with
            | Some (tx, ty, tMaze, isDone) -> (tx, ty, 'v', tMaze, isDone)
            | None -> goOn '^' x y maze
        | '^' ->
            match goLeft x y maze with
            | Some (tx, ty, tMaze, isDone) -> (tx, ty, '<', tMaze, isDone)
            | None -> goOn '>' x y maze
        | _ -> failwith "wrong orientation"
    | Right -> 
        match orientation with
        | '<' ->
            match goUp x y maze with
            | Some (tx, ty, tMaze, isDone) -> (tx, ty, '^', tMaze, isDone)
            | None -> goOn 'v' x y maze
        | '^' ->
            match goRight x y maze with
            | Some (tx, ty, tMaze, isDone) -> (tx, ty, '>', tMaze, isDone)
            | None -> goOn '<' x y maze
        | '>' ->
            match goDown x y maze with
            | Some (tx, ty, tMaze, isDone) -> (tx, ty, 'v', tMaze, isDone)
            | None -> goOn '^' x y maze
        | 'v' ->
            match goLeft x y maze with
            | Some (tx, ty, tMaze, isDone) -> (tx, ty, '<', tMaze, isDone)
            | None -> goOn '>' x y maze
        | _ -> failwith "wrong orientation"

let printAnswer maze =
    maze
    |> Array2D.iteri (fun y x c ->
        match x = width - 1 with
        | true -> printfn "%c" c
        | false -> printf "%c" c)

let rec run orientation x y maze =
    match goOn orientation x y maze with
    | (_, _, _, tMaze, true) -> printAnswer tMaze
    | (x, y, orientation, tMaze, _) -> run orientation x y tMaze
    ()

let maze1 = incrementCell x0 y0 maze0

let getFreeCell x y maze =
    match getCell x y maze with
    | None -> None
    | Some '#' -> None
    | Some freeCell -> Some freeCell 
    
let isTrapped x y maze =
    let upBloc = getFreeCell x (y + 1) maze
    let downBloc = getFreeCell x (y - 1) maze
    let leftBloc = getFreeCell (x - 1) y maze
    let rightBloc = getFreeCell (x + 1) y maze
    match (upBloc, downBloc, leftBloc, rightBloc) with
    | (None, None, None, None) -> true
    | _ -> false

match isTrapped x0 y0 maze1 with
| true ->
    let trappedMaze = Array2D.copy maze0
    Array2D.set trappedMaze y0 x0 '0'
    printAnswer trappedMaze
| false -> run orientation0 x0 y0 maze1