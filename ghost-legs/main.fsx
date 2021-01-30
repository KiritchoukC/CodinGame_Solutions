module String =
    let split splitChars (str: string) = str.Split splitChars
    let removeWhiteSpaces (str:string) = str |> Seq.filter (fun c -> not (c = ' ')) |> System.String.Concat


open System

let read _ = Console.In.ReadLine()

let arrayToTuple (token: string []) = (token.[0] |> int, token.[1] |> int)

let somes lst =
    lst
    |> List.filter Option.isSome
    |> List.map Option.get
    
let (W, H) =
    read ()
    |> String.split [| ' ' |]
    |> arrayToTuple

let inputLines =
    [ 0 .. H - 1 ] |> List.map read

let labels = inputLines.[0] |> String.removeWhiteSpaces

let destinations = inputLines.[H - 1] |> String.removeWhiteSpaces

let lines =
    inputLines
    |> List.skip 1
    |> List.take (H - 2)

let getConnectorIndices (line:string) =
    line
    |> Seq.toList
    |> List.mapi (fun index c ->
        match c with
        | '-' ->
            match line.[index + 1] with
            | '-' -> Some index
            | _ -> None
        | _ -> None)
    |> somes

let connectorIndexToLabelIndex index =
    index
    |> float
    |> (+) 1.
    |> (fun x -> x / 3.)
    |> Math.Ceiling
    |> (fun x -> x - 1.)
    |> int
    
let connectorIndexToConnectorTuple index =
    let sourceIndex = connectorIndexToLabelIndex index
    (labels.[sourceIndex], labels.[sourceIndex + 1])

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
    | [x] ->
        match x with
        | (left, right) when left = label -> snd x
        | (left, right) when right = label -> fst x
        | _ -> label
    | x :: rest ->
        match x with 
        | (source, target) when source = label -> findDestination rest target  // it goes from x to y
        | (source, target) when target = label -> findDestination rest source // it goes from y to x
        | _ -> findDestination rest label // it does not move
    | _ -> 'Z'
    
let mapLabelToDestination label =
    labels
    |> Seq.toList
    |> List.findIndex (fun c -> c = label)
    |> (fun i -> destinations.[i])
    
let answers =
    labels
    |> Seq.toList
    |> List.map (fun l -> (l, findDestination connectors l))
    |> List.map (fun ld -> (fst ld, mapLabelToDestination (snd ld)))

answers |> List.iter (fun c -> printfn "%c%c" (fst c) (snd c))
