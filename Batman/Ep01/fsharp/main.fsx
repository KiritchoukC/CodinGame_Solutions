open System

type Horizontal =
    { Low: int
      High: int }

type Vertical =
    { Low: int
      High: int }

type Limit =
    { Horizontal: Horizontal
      Vertical: Vertical }

let token = (Console.In.ReadLine()).Split [| ' ' |]
let W = int (token.[0])
let H = int (token.[1])
let N = int (Console.In.ReadLine())
let token1 = (Console.In.ReadLine()).Split [| ' ' |]
let X0 = int (token1.[0])
let Y0 = int (token1.[1])

let goUp ((x: int), (y: int), limit) =
    let nextY = int (Math.Floor(float (y - limit.Vertical.Low) / 2.)) + limit.Vertical.Low
    let newLimit = { limit with Vertical = { limit.Vertical with High = y } }
    (x, nextY, newLimit)

let goRight ((x: int), (y: int), limit) =
    let nextX = int (Math.Floor(float (limit.Horizontal.High - x) / 2.)) + x
    let newLimit = { limit with Horizontal = { limit.Horizontal with Low = x } }
    (nextX, y, newLimit)

let goDown ((x: int), (y: int), limit) =
    let nextY = int (Math.Floor(float (limit.Vertical.High - y) / 2.)) + y
    let newLimit = { limit with Vertical = { limit.Vertical with Low = y } }
    (x, nextY, newLimit)

let goLeft ((x: int), (y: int), limit) =
    let nextX = int (Math.Floor(float (x - limit.Horizontal.Low) / 2.)) + limit.Horizontal.Low
    let newLimit = { limit with Horizontal = { limit.Horizontal with High = x } }
    (nextX, y, newLimit)

let move bombDir =
    match bombDir with
    | "U" -> goUp
    | "UR" -> goUp >> goRight
    | "R" -> goRight
    | "DR" -> goDown >> goRight
    | "D" -> goDown
    | "DL" -> goDown >> goLeft
    | "L" -> goLeft
    | "UL" -> goUp >> goLeft
    | _ -> failwith "wrong input"

let rec findNextWindow x y limit =
    let bombDir = Console.In.ReadLine()

    match bombDir with
    | "" -> ()
    | _ ->
        let (nextX, nextY, newLimit) = move bombDir (x, y, limit)

        printfn "%i %i" (nextX) (nextY)
        findNextWindow nextX nextY newLimit
        ()

findNextWindow X0 Y0
    { Vertical =
          { Low = 0
            High = H }
      Horizontal =
          { Low = 0
            High = W } }
