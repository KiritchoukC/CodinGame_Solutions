contains :: Eq a => a -> [a] -> Bool
contains _ [] = False
contains e (x : xs)
  | e == x = True
  | otherwise = contains e xs