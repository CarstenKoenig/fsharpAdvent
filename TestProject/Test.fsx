#r @"../PrimesTypeProvider/bin/Debug/PrimesTypeProvider.dll"

type numbers = Numbers.NumbersProvider<"5">

let n5 = numbers.N5()
printfn "number: %d" n5.Value