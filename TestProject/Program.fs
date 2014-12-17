// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

type numbers = Numbers.NumbersProvider<"4,5">

let isPrime (o : obj) =
    match o with
    | :? PrimesTypeProvider.IPrime -> true
    | _                            -> false

[<EntryPoint>]
let main argv = 
    let n5 = numbers.N5()
    printfn "number: %d of type %A isPrime: %A" n5.Value (n5.GetType()) (isPrime n5)

    let n4 = numbers.N4()
    printfn "number: %d of type %A isPrime: %A" n4.Value (n4.GetType()) (isPrime n4)

    0 // return an integer exit code
