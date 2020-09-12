open System

let input = Console.In.ReadLine()
let states = Console.In.ReadLine()
let numberOfTransitions = int (Console.In.ReadLine())
let transitions =
    [ 0 .. numberOfTransitions - 1 ] |> List.map (fun _ -> Console.In.ReadLine())
let startState = Console.In.ReadLine() |> char
let endState = Console.In.ReadLine() |> fun s -> s.Split [|' '|] |> Array.toList
let numberOfWords = int (Console.In.ReadLine())
let words =
    [ 0 .. numberOfWords - 1 ] |> List.map (fun _ -> Console.In.ReadLine())

let findTransition state c =
    transitions
    |> List.filter (fun t -> t.[0] = state)
    |> List.tryFind (fun t -> t.Contains (string c))

let rec run state word index =
    match String.length word = index with
    | true -> Some state
    | false -> 
        let c = word.[index]
        let transition = findTransition state c
        match transition with
        | None -> None
        | Some t ->
            run t.[4] word (index+1)

let isRightEndState state =
    endState
    |> List.contains (string state) 

words
|> List.map (fun w -> run startState w 0)
|> List.map (fun o ->
    match o with
    | Some x when isRightEndState x -> true
    | _ -> false)
|> List.iter (fun a -> printfn "%A" a)
