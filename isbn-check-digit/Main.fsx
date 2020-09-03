open System

let read _ = Console.In.ReadLine()

let n = read () |> int

let isbns =
    [ 1 .. n ] |> List.map read

let filterSome lst =
    lst
    |> List.filter Option.isSome
    |> List.map Option.get

let charToInt (char: char) =
    char
    |> string
    |> int

let isbn10DigitToInt =
    function
    | 'X' -> 10
    | x -> charToInt x

let verify10 (isbn10: string) =
    let isWellFormatted =
        isbn10
        |> String.forall (fun c ->
            match Char.IsDigit c with
            | true -> true
            | false ->
                match c with
                | 'X' -> true
                | _ -> false)

    match isWellFormatted with
    | false -> Some isbn10
    | true ->
        let sum =
            [ 10 .. -1 .. 2 ]
            |> List.mapi (fun idx weight -> (isbn10DigitToInt isbn10.[idx]) * weight)
            |> List.sum

        let modulo = sum % 11

        let verificationToken = isbn10.[(String.length isbn10) - 1]

        let expectedToken =
            match 11 - modulo with
            | 11 -> '0'
            | 10 -> 'X'
            | x -> (string x).[0]

        match verificationToken = expectedToken with
        | true -> None
        | false -> Some isbn10

let verify13 (isbn13: string) =
    let isWellFormatted = isbn13 |> String.forall Char.IsDigit
    match isWellFormatted with
    | false -> Some isbn13
    | true ->
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

        let expectedToken =
            match 10 - modulo with
            | 10 -> 0
            | x -> x

        match verificationToken = expectedToken with
        | true -> None
        | false -> Some isbn13

let verify isbns =
    isbns
    |> List.map (fun x ->
        match String.length x with
        | 13 -> verify13 x
        | 10 -> verify10 x
        | _ -> Some x)

let printAnswer invalidIsbns =
    printfn "%i invalid:" (List.length invalidIsbns)
    invalidIsbns |> List.iter (fun invalidIsbn -> printfn "%s" (string invalidIsbn))
    ()


isbns
|> verify
|> filterSome
|> printAnswer
