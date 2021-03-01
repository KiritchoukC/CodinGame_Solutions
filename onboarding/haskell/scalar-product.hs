prodOfPair :: (Int, Int) -> Int
prodOfPair = uncurry (*)

prodOfPairs :: [(Int, Int)] -> [Int]
prodOfPairs = map prodOfPair

scalarProd :: [Int] -> [Int] -> Int
scalarProd xs ys = sum (prodOfPairs (zip xs ys))