module String =
    let split separator (str: string) = str.Split separator

open System

let read () = Console.In.ReadLine()

let extractDataFromString = String.split [| ' ' |] >> Array.map int

let (w, h, countX, countY) =
//    read ()
    "10 5 2 1"
    |> extractDataFromString
    |> (fun x -> (x.[0], x.[1], x.[2], x.[3]))

//let xs = read () |> extractDataFromString
let xs = "2 5" |> extractDataFromString

//let ys = read () |> extractDataFromString
let ys = "3" |> extractDataFromString


