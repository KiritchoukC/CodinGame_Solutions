open System

module Domain =
    type Coords =
        { X: int
          Y: int }

    type State =
        { Light: Coords
          Thor: Coords }

        static member Create lightX lightY thorX thorY =
            { Light =
                  { X = lightX
                    Y = lightY }
              Thor =
                  { X = thorX
                    Y = thorY } }

        member this.Move dir =
            match dir with
            | "N" ->
                { this with
                      Thor =
                          { X = this.Thor.X
                            Y = this.Thor.Y - 1 } }
            | "NE" ->
                { this with
                      Thor =
                          { X = this.Thor.X + 1
                            Y = this.Thor.Y - 1 } }
            | "E" ->
                { this with
                      Thor =
                          { X = this.Thor.X + 1
                            Y = this.Thor.Y } }
            | "SE" ->
                { this with
                      Thor =
                          { X = this.Thor.X + 1
                            Y = this.Thor.Y + 1 } }
            | "S" ->
                { this with
                      Thor =
                          { X = this.Thor.X
                            Y = this.Thor.Y + 1 } }
            | "SW" ->
                { this with
                      Thor =
                          { X = this.Thor.X - 1
                            Y = this.Thor.Y + 1 } }
            | "W" ->
                { this with
                      Thor =
                          { X = this.Thor.X - 1
                            Y = this.Thor.Y } }
            | "NW" ->
                { this with
                      Thor =
                          { X = this.Thor.X - 1
                            Y = this.Thor.Y - 1 } }
            | x -> failwithf "Wrong input: %s" x

module Data =

    let (lightX, lightY, initialTX, initialTY) =
        let inputs = (Console.In.ReadLine()).Split [| ' ' |] |> Array.map int
        (inputs.[0], inputs.[1], inputs.[2], inputs.[3])

module Application =
    open Domain
    open Data

    let rec run (state: State): unit =
        let nextDirection =
            match (compare state.Light.X state.Thor.X, compare state.Light.Y state.Thor.Y) with
            | (0, 0) -> ""
            | (1, 0) -> "E"
            | (-1, 0) -> "W"
            | (0, 1) -> "S"
            | (0, -1) -> "N"
            | (1, 1) -> "SE"
            | (1, -1) -> "NE"
            | (-1, 1) -> "SW"
            | (-1, -1) -> "NW"
            | x -> failwithf "Unexpected value %A" x

        printfn "%s" nextDirection
        run (state.Move nextDirection)

    run (State.Create lightX lightY initialTX initialTY)
