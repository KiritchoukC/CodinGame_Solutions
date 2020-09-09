module String =
    let split splitChars (str: string) = str.Split splitChars


open System

let read _ = Console.In.ReadLine()

let arrayToTuple (token: string []) = (token.[0] |> int, token.[1] |> int)

let somes lst =
    lst
    |> List.filter Option.isSome
    |> List.map Option.get
    
let (W, H) =
    //    read ()
    "7 7"
    |> String.split [| ' ' |]
    |> arrayToTuple

//let inputLines =
//    [ 0 .. H - 1 ] |> List.map read
    
let inputLines = [
    "A  B  C"
    "|  |  |"
    "|--|  |"
    "|  |--|"
    "|  |--|"
    "|  |  |"
    "1  2  3"
]

let labels = inputLines.[0] |> Seq.filter (fun c -> not (c = ' ')) |> String.Concat

let destinations = inputLines.[H - 1]

let lines =
    inputLines
    |> List.skip 1
    |> List.take (H - 2)

let getConnectorIndices line =
    line
    |> Seq.toList
    |> List.mapi (fun index c ->
        match c with
        | '-' -> Some index
        | _ -> None)
    |> somes

let connectorIndexToLabel index =
    index
    |> float
    |> (+) 1.
    |> (fun x -> x / 3.)
    |> Math.Ceiling
    |> (-) 1.
    |> int

let connectorIndexToConnectorTuple index =
    let sourceIndex = connectorIndexToLabel index
    let alphabet = [ 'A' .. 'Z' ]
    (alphabet.[sourceIndex], alphabet.[sourceIndex + 1])

let getConnector line =
    let hasConnector = String.exists (fun c -> c = '-') line
    match hasConnector with
    | false -> None
    | true ->
        line
        |> getConnectorIndices
        |> List.map connectorIndexToConnectorTuple
        |> Some

let connectors =
    lines
    |> List.map getConnector
    |> somes
    |> List.fold (fun acc item -> acc @ item) []


let rec findDestination connectors label =
    match connectors with
    | [x] -> snd x
    | x :: rest ->
        match x with 
        | (x, y) when x = label -> findDestination rest y // it goes from x to y
        | (x, y) when y = label -> findDestination rest x // it goes from y to x
        | _ -> findDestination rest label // it does not move
    | _ -> 'Z' 

let answers =
    labels
    |> String.map (fun l -> findDestination connectors l)
    |> Seq.toList

answers |> List.iter (fun c -> printfn "%c" c)
