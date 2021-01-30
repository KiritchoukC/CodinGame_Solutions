open System

let readInputs nbInputs = List.map (fun i -> (i, Console.In.ReadLine())) nbInputs

let findHighestMountain mountains =
    List.reduce (fun acc value ->
        match snd value > snd acc with
        | true -> value
        | false -> acc) mountains
    
let printAnswer answer = printfn "%i" (fst answer)

let rec run () =
    [ 0 .. 7 ]
    |> readInputs
    |> findHighestMountain
    |> printAnswer

    run ()

run ()
