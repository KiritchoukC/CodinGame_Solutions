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
            
    let (|EmptyCell|_|) input =
        match input with
        | '.' -> Some(EmptyCell)
        | _ -> None

module Data =
    let private read _ = Console.In.ReadLine()
    let private readInt _ = read () |> int
    let private width = readInt ()
    let private height = readInt ()
    let private lines =
        List.init height read
    let data =
        Array2D.init width height (fun x y -> lines.[y].[x])

module Application =
    open Data
    open Domain
    
    let rec getRight state =
        let nextX = state.X + 1
        let rightNeighbor = Array2D.tryGet nextX state.Y state.Data
        match rightNeighbor with
        | None -> state.AddNoneToResult()
        | Some EmptyCell -> getRight { state with X = nextX }
        | Some _ -> state.AddToResult(sprintf "%i %i" nextX state.Y)
        
    let rec getBelow state =
        let nextY = state.Y + 1
        let belowNeighbor = Array2D.tryGet state.X nextY state.Data
        match belowNeighbor with
        | None -> state.AddNoneToResult()
        | Some cell when cell = '.' -> getBelow { state with Y = nextY }
        | Some _ -> state.AddToResult(sprintf "%i %i" state.X nextY)
        
    let findNeighbors state: State option =
        let currentCell = Array2D.tryGet state.X state.Y state.Data
        match currentCell with
        | None -> Some state
        | Some cell ->
            match cell with
            | '.' -> None
            | _ ->
                let stateWithCurrentCell = state.AddToResult(sprintf "%i %i" state.X state.Y)

                let stateWithRightNeighbor = getRight stateWithCurrentCell
                
                let stateWithBelowNeighbor = getBelow { stateWithRightNeighbor with X = state.X}
                
                stateWithBelowNeighbor |> Some

    data
    |> Array2D.iteri (fun x y c ->
        let stateOpt = findNeighbors (State.Init data x y)
        match stateOpt with
        | None -> ()
        | Some state -> printfn "%s" state.Result)
