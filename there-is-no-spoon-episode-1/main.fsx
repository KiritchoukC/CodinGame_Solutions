open System

[<RequireQualifiedAccess>]
module Array2D =
    let tryGet x y a =
        try
            Array2D.get a x y |> Some
        with :? IndexOutOfRangeException -> None


module Domain =
    type State =
        { Data: char [,]
          X: int
          Y: int
          Result: string }

        member this.AddNoneToResult() =
            { Data = this.Data
              X = this.X
              Y = this.Y
              Result = this.Result + " -1 -1" }

        member this.AddToResult str =
            { Data = this.Data
              X = this.X
              Y = this.Y
              Result = this.Result + " " + str }

        static member Init a x y =
            { Data = a
              X = x
              Y = y
              Result = "" }

module Data =
    let read _ = Console.In.ReadLine()
    let readInt _ = read () |> int
    let private width = readInt ()
    let private height = readInt ()
    let private lines =
        List.init height read
    let data =
        Array2D.init width height (fun x y -> lines.[y].[x])

//    let data: char [,] =
//        let temp = Array2D.create 2 2 '0'
//        Array2D.set temp 1 1 '.'
//        temp

module Application =
    open Data
    open Domain

    let findNeighbors state: State option =
        let currentCell = Array2D.tryGet state.X state.Y state.Data
        match currentCell with
        | None -> Some state
        | Some cell ->
            match cell with
            | '.' -> None
            | _ ->
                let stateWithCurrentCell = state.AddToResult(sprintf "%i %i" state.X state.Y)
                let rightNeighbor = Array2D.tryGet (state.X + 1) state.Y state.Data

                let stateWithRightNeighbor =
                    match rightNeighbor with
                    | None ->
                        stateWithCurrentCell.AddNoneToResult()
                    | Some x when x = '.' ->
                        stateWithCurrentCell.AddNoneToResult()
                    | Some x  ->
                        stateWithCurrentCell.AddToResult(sprintf "%i %i" (state.X + 1) state.Y)

                let belowNeighbor = Array2D.tryGet state.X (state.Y + 1) state.Data
                match belowNeighbor with
                | None ->
                    stateWithRightNeighbor.AddNoneToResult() |> Some
                | Some x when x = '.' ->
                    stateWithRightNeighbor.AddNoneToResult() |> Some
                | Some x -> stateWithRightNeighbor.AddToResult(sprintf "%i %i" state.X (state.Y + 1)) |> Some

    
    data
    |> Array2D.iteri (fun x y c ->
        let stateOpt = findNeighbors (State.Init data x y)
        match stateOpt with
        | None -> ()
        | Some state -> printfn "%s" state.Result)
