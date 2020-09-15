module Domain =
    type Plan =
        { Id: int
          PlanMap: char [,] }

    type CompletePlan =
        { Id: int
          Steps: int }

    type OutputPlan =
        | Complete of CompletePlan
        | Incomplete

    type Orientation =
        | Up
        | Down
        | Right
        | Left

    let charToOrientation c =
        match c with
        | 'v' -> Down
        | '^' -> Up
        | '<' -> Left
        | '>' -> Right
        | _ -> failwith "Bad data"

    let rec traversePlan (plan: Plan) x y steps =
        let currentCell = Array2D.get plan.PlanMap y x
        match currentCell with
        | '#' -> failwith "BAM! You hit a wall!"
        | 'T' -> Complete { Id = plan.Id; Steps = steps }
        | '.' -> Incomplete
        | _ ->
            let orientation = charToOrientation currentCell
            match orientation with
            | Up -> traversePlan plan x (y - 1) (steps + 1)
            | Down -> traversePlan plan x (y + 1) (steps + 1) 
            | Left -> traversePlan plan (x - 1) y (steps + 1)
            | Right -> traversePlan plan (x + 1) y (steps + 1)


    let checkPlan x0 y0 (plan: Plan) =
        traversePlan plan x0 y0 0

module Data =

    open System
    open Domain

    let private getData () =
        (Console.In.ReadLine()).Split [| ' ' |]
        |> Array.map int
        |> fun token -> (token.[0], token.[1])

    let (w, h) = getData ()

    let (startRow, startCol) = getData ()

    let n = Console.In.ReadLine() |> int

    let private getPlan n =
        let lines =
            [ 1 .. h ] |> List.map (fun _ -> Console.In.ReadLine())
        { Id = n
          PlanMap = Array2D.init h w (fun x y -> lines.[x].[y]) }

    let plans =
        [ 0 .. n - 1 ] |> List.map (fun planIndex -> getPlan planIndex)


module Application =
    open Data
    open Domain

    let printAnswer plan = printfn "%i" plan.Id

    let checkedPlans =
        plans
        |> List.map (checkPlan startCol startRow)
        |> List.choose (fun x ->
            match x with
            | Complete cp -> Some cp
            | Incomplete -> None)
    
    match checkedPlans with
    | [] -> printfn "TRAP"
    | lst ->
        lst
        |> List.minBy (fun x -> x.Steps)
        |> printAnswer
