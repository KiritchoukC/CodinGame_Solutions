open System

let N = int(Console.In.ReadLine()) (* Number of elements which make up the association table. *)
let Q = int(Console.In.ReadLine()) (* Number Q of file names to be analyzed. *)

let mimes = 
    [0 .. N - 1]
    |> List.map (fun _ -> Console.In.ReadLine())
    |> List.map (fun input -> input.Split [|' '|])
    |> List.map (fun token -> (token.[0].ToLower(), token.[1]))
    
let getExtFromFileName (fileName: string) =
    match fileName.Contains(".") with
    | true -> 
        let split = fileName.Split [|'.'|]
        Some (split.[(Array.length split) - 1])
    | false -> None
    
let getMime (mimes: (string*string) list) (ext: string option) =
    match ext with
    | None -> None
    | Some ext -> 
        mimes
        |> List.tryFind (fun t -> if ext = (fst t) then true else false)
        |> Option.bind (fun x -> Some(snd x))
    
let printAnswer mime =
    match mime with
    | None -> "UNKNOWN"
    | Some x -> x
    |> printfn "%s"
    
[0 .. Q - 1]
|> List.map (fun _ -> Console.In.ReadLine())
|> List.map getExtFromFileName
|> List.map (Option.bind (fun x -> Some(x.ToLower())))
|> List.map (getMime mimes)
|> List.iter printAnswer

