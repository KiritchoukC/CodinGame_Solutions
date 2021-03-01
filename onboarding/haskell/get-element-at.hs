getElementAt :: [a] -> Int -> a
getElementAt [] _ = error "Out of range"
getElementAt (x : _) 0 = x
getElementAt (_ : xs) i = getElementAt xs (i - 1)