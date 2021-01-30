import System.IO

main :: IO ()
main = do
  hSetBuffering stdout NoBuffering -- DO NOT REMOVE
  loop

enemyToAttack :: Ord a => (a, p) -> (a, p) -> p
enemyToAttack e1 e2 = if fst e1 < fst e2 then snd e1 else snd e2

loop :: IO ()
loop = do
  input_line <- getLine
  let enemy1 = input_line :: String -- name of enemy 1
  input_line <- getLine
  let dist1 = read input_line :: Int -- distance to enemy 1
  input_line <- getLine
  let enemy2 = input_line :: String -- name of enemy 2
  input_line <- getLine
  let dist2 = read input_line :: Int -- distance to enemy 2
  putStrLn $ enemyToAttack (dist1, enemy1) (dist2, enemy2)

  loop