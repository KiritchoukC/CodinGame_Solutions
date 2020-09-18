open System

let n = int (Console.In.ReadLine()) (* the number of temperatures to analyse *)

match n with
| 0 -> printfn "%i" 0
| _ -> 
    let temperatures =
        (Console.In.ReadLine()).Split [| ' ' |]
        |> Array.toList
        |> List.map int

    let getClosestTo0 t1 t2 =
        match (abs t1) = (abs t2) with
        | true ->
            if t1 > 0 then t1 else t2
        | false ->
            match (abs t1) > (abs t2) with
            | true -> t2
            | false -> t1

    temperatures
    |> List.reduce getClosestTo0
    |> printfn "%i"
