(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

module Domain =
    type Distance =
        | VeryFar of int
        | Far of int
        | Middle of int
        | Close of int
        | VeryClose of int
        
        static member Create input =
            match input with
            | x when x > 8000 -> VeryFar input
            | x when x > 5000 -> Far input
            | x when x > 3000 -> Middle input
            | x when x > 1000 -> Close input
            | _ -> VeryClose input

module Application =
    open Domain
    
    let calculateThrust dist angle =
        match (dist, abs angle) with
        | (d, a) when d > 3000 && a < 10 -> 100
        | (d, a) when d < 3000 && a < 10 -> 50
        | (d, a) when d < 3000 && a > 60 -> 10
        | (d, a) when d < 500 || a > 60 -> 20
        | (d, a) when d < 500 || a > 60 -> 20
        | (d, a) -> 100
        
    
    (* game loop *)
    while true do
        (* nextCheckpointX: x position of the next check point *)
        (* nextCheckpointY: y position of the next check point *)
        (* nextCheckpointDist: distance to the next checkpoint *)
        (* nextCheckpointAngle: angle between your pod orientation and the direction of the next checkpoint *)
        let token = (Console.In.ReadLine()).Split [|' '|]
        let x = int(token.[0])
        let y = int(token.[1])
        let nextCheckpointX = int(token.[2])
        let nextCheckpointY = int(token.[3])
        let nextCheckpointDist = int(token.[4])
        let nextCheckpointAngle = int(token.[5])
        let token1 = (Console.In.ReadLine()).Split [|' '|]
        let opponentX = int(token1.[0])
        let opponentY = int(token1.[1])
        
        (* Write an action using printfn *)
        (* To debug: eprintfn "Debug message" *)
        
        eprintfn "angle %i" nextCheckpointAngle
        eprintfn "dist %i" nextCheckpointDist

        let thrust = calculateThrust nextCheckpointDist nextCheckpointAngle
        
        (* You have to output the target position *)
        (* followed by the power (0 <= thrust <= 100) *)
        (* i.e.: "x y thrust" *)
        printfn "%d %d %d" nextCheckpointX nextCheckpointY thrust
        ()
