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

    type Angle =
        | Zero of int
        | AlmostZero of int
        | Perpendicular of int
        | Right of int
        | WrongSide of int

        static member Create input =
            match abs input with
            | x when x > 100 -> WrongSide input
            | x when x > 60 -> Right input
            | x when x > 30 -> Perpendicular input
            | x when x > 10 -> AlmostZero input
            | _ -> Zero input

module Application =
    open Domain

    let readInts _ = (Console.In.ReadLine()).Split [| ' ' |] |> Array.map int

    let calculateThrust (dist: Distance) (angle: Angle) =
        match (dist, angle) with
        | VeryFar _, Zero _ -> "BOOST"
        | VeryFar _, AlmostZero _ -> "100"
        | VeryFar _, Perpendicular _ -> "100"
        | VeryFar _, Right _ -> "100"
        | VeryFar _, WrongSide _ -> "0"
        | Far _, Zero _ -> "100"
        | Far _, AlmostZero _ -> "100"
        | Far _, Perpendicular _ -> "100"
        | Far _, Right _ -> "100"
        | Far _, WrongSide _ -> "0"
        | Middle _, Zero _ -> "100"
        | Middle _, AlmostZero _ -> "100"
        | Middle _, Perpendicular _ -> "100"
        | Middle _, Right _ -> "100"
        | Middle _, WrongSide _ -> "0"
        | Close _, Zero _ -> "100"
        | Close _, AlmostZero _ -> "100"
        | Close _, Perpendicular _ -> "80"
        | Close _, Right _ -> "100"
        | Close _, WrongSide _ -> "0"
        | VeryClose _, Zero _ -> "100"
        | VeryClose _, AlmostZero _ -> "100"
        | VeryClose _, Perpendicular _ -> "100"
        | VeryClose _, Right _ -> "60"
        | VeryClose _, WrongSide _ -> "0"


    (* game loop *)
    while true do
        (* nextCheckpointX: x position of the next check point *)
        (* nextCheckpointY: y position of the next check point *)
        (* nextCheckpointDist: distance to the next checkpoint *)
        (* nextCheckpointAngle: angle between your pod orientation and the direction of the next checkpoint *)
        let token = readInts ()
        let x = token.[0]
        let y = token.[1]
        let nextCheckpointX = token.[2]
        let nextCheckpointY = token.[3]
        let nextCheckpointDist = token.[4]
        let nextCheckpointAngle = token.[5]
        let (opponentX, opponentY) =
            readInts () |> fun x -> x.[0], x.[1]

        (* Write an action using printfn *)
        (* To debug: eprintfn "Debug message" *)

        eprintfn "angle %i" nextCheckpointAngle
        eprintfn "dist %i" nextCheckpointDist

        let thrust = calculateThrust (Distance.Create nextCheckpointDist) (Angle.Create nextCheckpointAngle)

        (* You have to output the target position *)
        (* followed by the power (0 <= thrust <= 100) *)
        (* i.e.: "x y thrust" *)
        printfn "%d %d %s" nextCheckpointX nextCheckpointY thrust
        ()
