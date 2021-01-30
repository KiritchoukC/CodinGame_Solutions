open System
open System.Text.RegularExpressions

module Domain =
    let (|IsNumber|_|) input =
        let matchResult =  Regex.Match(input, "-?[0-9]+")
        if matchResult.Success then Some (input |> int) else None
    let (|IsReference|_|) input =
        let matchResult =  Regex.Match(input, "\$[0-9]+")
        if matchResult.Success then Some (input |> int) else None
    
    type Arg =
        | Nothing
        | Reference of int
        | Number of int
        static member create arg =
            match arg with
            | "_" -> Nothing
            | IsNumber x -> Number x
            | IsReference x -> Reference x
            | _ -> failwithf "Unexpected arg %s" arg
    
    type CellContent = {
        Arg1: Arg
        Arg2: Arg
    }
    
    type Cell =
        | Value of CellContent
        | Add of CellContent
        | Substract of CellContent
        | Multiplication of CellContent
        member this.Value =
            match this with
            | Value { Arg1 = value } -> value
            | Add a -> a.Arg1 + a.Arg2
            | Substract s -> s.Arg1 - s.Arg2
            | Multiplication m -> m.Arg1 * m.Arg2
            
        static member create operation arg1 arg2 =
            match operation with
            | "VALUE" -> Value {Arg1 = Arg.create arg1; Arg2 = Arg.create arg2}
            | "ADD" -> Add {Arg1 = Arg.create arg1; Arg2 = Arg.create arg2}
            | "SUB" -> Substract {Arg1 = Arg.create arg1; Arg2 = Arg.create arg2}
            | "MULT" -> Multiplication {Arg1 = Arg.create arg1; Arg2 = Arg.create arg2}
            | _ -> failwithf "Unexpected operation %s" operation
            
module Data =
    open Domain
    
    let read _ = Console.In.ReadLine()
    let readInt _ = read () |> int

    let n = readInt ()

    let cells =
        [ 0 .. n - 1 ]
        |> List.map read
        |> List.map (fun x -> x.Split [|' '|])
        |> List.map (fun t -> Cell.create t.[0] t.[1] t.[2])

module Application =
    open Domain
    open Data
    
    let rec getArgValue (data: Cell list) arg =
        match arg with
        | Nothing -> 0
        | Number n -> n
        | Reference r -> getArgValue data data.[r]
    
    let getCellValue (data: Cell list) cell =
        match cell with
        | Value x -> getArgValue data x.Arg1
        | Add x -> (getArgValue data x.Arg1) + (getArgValue data x.Arg2)
        | Substract x ->  (getArgValue data x.Arg1) - (getArgValue data x.Arg2)
        | Multiplication x ->  (getArgValue data x.Arg1) * (getArgValue data x.Arg2)
        
    
    cells
    |> List.map (getCellValue cells)
