-- Lecture 7
-- Express the comprehension [f x | x <- xs, p x] using map and filter
-- solution1 :: [a] -> [a]

baseline :: (t -> a) -> (t -> Bool) -> [t] -> [a]
baseline f p xs = [f x | x <- xs, p x]

solution1 :: (t -> a) -> (t -> Bool) -> [t] -> [a]
solution1 f p = map f . filter p

-- Redefine map f and filter p using foldr
-- map' :: (a -> a) -> [a] -> [a]
map' f = foldr (\x xs -> f x : xs) []

map'' f = foldr ((:) . f) []

filter' p = foldr (\x xs -> if p x then x : xs else xs) []