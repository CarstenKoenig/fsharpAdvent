// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

type N5 = Numbers.NumbersProvider<"5">

let isPrime (o : obj) =
    match o with
    | :? PrimesTypeProvider.IPrime -> true
    | _                            -> false

[<EntryPoint>]
let main argv = 
    let n5 = N5()
    printfn "number: %d of type %A isPrime: %A" n5.Value (n5.GetType()) (isPrime n5)

    0 // return an integer exit code
