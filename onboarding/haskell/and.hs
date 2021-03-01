and :: [Bool] -> Bool
and [] = True
and [True] = True
and [False] = False
and (x : xs) = (&&) x (Main.and xs)
