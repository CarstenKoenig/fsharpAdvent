#r @"../PrimesTypeProvider/bin/Debug/PrimesTypeProvider.dll"

let n5 = Numbers.N5 ()
printfn "number: %d" n5.Value

let i5 = n5 :> Numbers.INum
printfn "number: %d" n5.Value

