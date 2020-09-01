[<RequireQualifiedAccess>]
module String =
    let removeLastChar (str: string) = str.Remove((String.length str) - 1)
    let append (str2: string) (str1: string) = str1 + str2

[<RequireQualifiedAccessAttribute>]
module List =
    let concatenateTuple (t1, t2) = t1 @ t2
    let someOnly lst =
        lst 
        |> List.filter Option.isSome
        |> List.map Option.get

open System

let read _ = Console.In.ReadLine()

let n = int (read ())
    
let isbns =
    [1..n]
    |> List.map read

let isIsbn10 isbn =
    match String.length isbn with
    | 10 -> true
    | _ -> false
    
let isLengthValid isbn = 
    match String.length isbn with
        | 10 | 13 -> true
        | _ -> false
        
let keepValidLength isbns = isbns |> List.filter isLengthValid
        
let split10And13 isbns = isbns |> List.partition isIsbn10
    
let charToInt (char: char) = char |> string |> int

let isbn10DigitToInt =
    function
    | 'X' -> 10
    | x -> charToInt x

let verify10 (isbn10: string) =
    let sum =
        [ 10..-1..2 ]
        |> List.mapi (fun idx weight -> (isbn10DigitToInt isbn10.[idx]) * weight)
        |> List.sum
    
    let modulo = sum % 11
        
    let verificationToken = isbn10.[(String.length isbn10) - 1]
    
    let expectedToken =
        match 11 - modulo with
        | 10 -> 'X'
        | x -> (string x).[0]
    
    match verificationToken = expectedToken with
    | true -> None
    | false -> Some isbn10

let verify13 (isbn13: string) =
    let hasX = String.exists (fun x -> x = 'X') isbn13
    match hasX with
    | true -> Some isbn13
    | false ->
        let sum =
            [ 0 .. 11 ]
            |> List.map (fun idx ->
                match idx % 2 with
                | 0 ->
                    let tmp = charToInt isbn13.[idx]
                    tmp
                | _ ->
                    let tmp = 3 * (charToInt isbn13.[idx])
                    tmp)
            |> List.sum
        
        let modulo = sum % 10
        
        let verificationToken = isbn13.[(String.length isbn13) - 1] |> charToInt
        
        let expectedToken = 10 - modulo
        
        match verificationToken = expectedToken with
        | true -> None
        | false -> Some isbn13

let verify (isbns10, isbns13) =
    let verified10 =
        isbns10 |> List.map verify10
    let verified13 =
        isbns13 |> List.map verify13
    (verified10, verified13)

let printAnswer invalidIsbns invalidLengthIsbns =
    let answer = invalidIsbns @ invalidLengthIsbns
    printfn "%i invalid:" (List.length answer)
    answer |> List.iter (fun invalidIsbn -> printfn "%s" (string invalidIsbn))
    ()

let invalidLengthIsbns =
    isbns
    |> List.filter (isLengthValid >> not)

isbns
|> keepValidLength
|> split10And13
|> verify
|> List.concatenateTuple
|> List.someOnly
|> printAnswer invalidLengthIsbns
