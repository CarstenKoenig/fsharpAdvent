// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open PrimesTypeProvider

type numbers = Numbers.NumbersProvider<"4,5">

let mod' a b =
    match a % b with
    | pos when pos >= 0 -> pos
    | neg                -> -neg

let isPrime (o : obj) =
    match o with
    | :? IPrime -> true
    | _         -> false

let rec extEulkid a b =
    match (a,b) with
    | (a,0) -> (a,1,0)
    | (a,b) ->
        let (d,s,t) = extEulkid b (mod' a b)
        (d,t,s - t * (a / b))

let invMod<'p when 'p :> IPrime and 'p : (new : unit -> 'p)> = 
    let p = (new 'p()).GetValue()
    fun a ->
        let (_,a',_) = extEulkid a p
        mod' a' p

type Mod<'n when 'n :> INum and 'n : (new : unit -> 'n)> (x : int) =
    let n = (new 'n ()).GetValue()
    let value = mod' x n

    member private __.Value      = value
    override        __.ToString() = sprintf "%d (MOD %d)" value n

    static member (+) (a : Mod<'n>, b : Mod<'n>) =
        Mod<'n> (a.Value + b.Value)

    static member ( * ) (a : Mod<'n>, b : Mod<'n>) =
        Mod<'n> (a.Value * b.Value)

    static member (-) (a : Mod<'n>, b : Mod<'n>) =
        Mod<'n> (a.Value - b.Value)

type ModF<'p when 'p :> IPrime and 'p : (new : unit -> 'p)> (x : int) =
    let p = (new 'p ()).GetValue()
    let value = mod' x p

    member private __.Value      = value
    override        __.ToString() = sprintf "%d (MOD %d)" value p

    static member (+) (a : ModF<'p>, b : ModF<'p>) =
        ModF<'n> (a.Value + b.Value)

    static member ( * ) (a : ModF<'p>, b : ModF<'p>) =
        ModF<'n> (a.Value * b.Value)

    static member (-) (a : ModF<'p>, b : ModF<'p>) =
        ModF<'n> (a.Value - b.Value)

    static member (/) (a : ModF<'p>, b : ModF<'p>) =
        ModF<'p> (a.Value * invMod<'p> b.Value)

let modF<'p when 'p :> IPrime and 'p : (new : unit -> 'p)> x = 
    ModF<'p> x

type N5 = numbers.N5
type N4 = numbers.N4

[<EntryPoint>]
let main argv = 

    let n5 = N5()
    printfn "number: %d of type %A isPrime: %A" n5.Value (n5.GetType()) (isPrime n5)

    let i5 = invMod<N5> 3
    printfn "the inverse in Mod5 to 3 is %d" i5

    let n4 = N4()
    printfn "number: %d of type %A isPrime: %A" n4.Value (n4.GetType()) (isPrime n4)

    let x = modF<N5> 4 / modF<N5> 3
    printfn "4 / 3 = %A" x

    0 // return an integer exit code
