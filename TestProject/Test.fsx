#r @"../PrimesTypeProvider/bin/Debug/PrimesTypeProvider.dll"

type N5 = Numbers.NumbersProvider<"5">

let n5 = N5()
printfn "number: %d" n5.Value