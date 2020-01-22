module RationalCalc

open MathNet.Numerics

let rec GCD a b =
    if b = 0 then 
        a
    else
        GCD b (a % b)

let LCM a  b =
    a * b / GCD a b

let anglesEqual = 0

let angle x y = 
    
    BigRational.FromIntFraction