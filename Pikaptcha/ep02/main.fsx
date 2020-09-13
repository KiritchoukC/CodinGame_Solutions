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

//let (width, height) =
//    (Console.In.ReadLine()).Split [| ' ' |]
//    |> Array.map int
//    |> fun c -> (c.[0], c.[1])
//
//let lines = [ 0 .. height - 1 ] |> List.map (fun c -> Console.In.ReadLine())
//
//let side =
//    match Console.In.ReadLine() with
//    | "R" -> Right
//    | "L" -> Left
//    | _ -> failwith "Wrong side input"

let (width, height) = (5, 3)
let lines = [ ">000#"; "#0#00"; "00#0#" ]

let side = Left

let maze0 = Array2D.init height width (fun y x -> lines.[y].[x])

let (x0, y0, orientation0) =
    [ '>'; '<'; '^'; 'v' ]
    |> List.map (fun o ->
        let source = Array2D.find o maze0
        match source with
        | None -> None
        | Some coords -> Some(fst coords, snd coords, o))
    |> List.filter Option.isSome
    |> List.map Option.get
    |> List.head

let incrementCell x y maze =
    let mazeCopy = Array2D.copy maze
    let current = Array2D.get mazeCopy y x

    match current with
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
    let isOut = x = width || y = height || x < 0 || y < 0
    match isOut with
    | true -> None
    | _ ->
        let nextCell = Array2D.get maze y x
        let isDone =
            not (nextCell = '0') && (x = x0 && y = y0)
        match isDone with
        | true -> Some(0, 0, maze, true)
        | false ->
            match nextCell with
            | '#' -> None
            | c ->
                let updatedMaze = incrementCell x y maze
                Some(x, y, updatedMaze, false)

let goRight x y = go (x + 1) y
let goLeft x y = go (x - 1) y
let goDown x y = go x (y + 1)
let goUp x y = go x (y - 1)

let rec goOn orientation x y maze =
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

run orientation0 x0 y0 maze1
