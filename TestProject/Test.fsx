#r @"../PrimesTypeProvider/bin/Debug/PrimesTypeProvider.dll"

type numbers = Numbers.PrimesProvider<"4,5,13,256">

let n5 = numbers.N5()
printfn "number: %d" n5.Value

let i5 = n5 :> Numbers.INum
printfn "number: %d" n5.Value
