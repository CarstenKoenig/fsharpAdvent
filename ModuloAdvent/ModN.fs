(*
                                                       *
     *                                                          *
                                  *                  *        .--.
      \/ \/  \/  \/                                        ./   /=*
        \/     \/      *            *                ...  (_____)
         \ ^ ^/                                       \ \_((O.O))-.     *
         (o)(O)--)--------\.                           \   (   ) \  \._.
         |    |  ||================((~~~~~~~~~~~~~~~~~))|   ( )   |     \
          \__/             ,|        \. * * * * * * ./  (~~~~~~~~~~~)    \
   *        ||^||\.____./|| |          \___________/     ~||~~~~|~'\____/ *
            || ||     || || V            ||    ||         ||    |   
     *      <> <>     <> <>          (___||____||_____)  ((~~~~~|   *

*)

namespace ModuloAdvent

open PrimesTypeProvider

[<AutoOpen>]
module Operations =

    let mod' a b =
        match a % b with
        | pos when pos >= 0 -> pos
        | neg                -> -neg

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

    static member create x = Mod<'n> x


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

    static member create x = ModF<'p> x
