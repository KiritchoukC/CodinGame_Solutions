module String =
    let split separator (str: string) = str.Split separator

open System


let filterSome lst =
    lst
    |> List.filter Option.isSome
    |> List.map Option.get

let read () = Console.In.ReadLine()

let extractDataFromString = String.split [| ' ' |] >> Array.map int >> Array.toList

let (w, h, countX, countY) =
    read ()
    |> extractDataFromString
    |> (fun x -> (x.[0], x.[1], x.[2], x.[3]))

let xs = read () |> extractDataFromString

let ys = read () |> extractDataFromString
    
let getAllLengths ns n = 
    ns
    |> List.filter (fun n' -> n' < n)
    |> List.map (fun n' -> n - n')

let allXs = xs |> (@) [w]

let allYs = ys |> (@) [h]

let xLengths =
    allXs
    |> List.map (getAllLengths xs)
    |> List.concat
    |> (@) allXs
    
let yLengths =
    allYs
    |> List.map (getAllLengths ys)
    |> List.concat
    |> (@) allYs
    
xLengths
|> List.map (fun x ->
    yLengths
    |> List.filter (fun y -> y = x))
|> List.concat
|> List.length
|> printfn "%i"
