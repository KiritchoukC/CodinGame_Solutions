or :: Bool -> Bool -> Bool
or True _ = True
or _ True = True
or False False = False