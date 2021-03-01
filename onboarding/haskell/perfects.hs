facts :: Int -> [Int]
facts n = [x | x <- [1 .. n], n `mod` x == 0]

perfects :: Int -> [Int]
perfects n = [x | x <- [1 .. n], sum (facts x) - x == x]