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

#r @"../PrimesTypeProvider/bin/Debug/PrimesTypeProvider.dll"
#r @"./bin/Debug/ModuloAdvent.exe"

open PrimesTypeProvider
open ModuloAdvent

type numbers = Numbers.NumbersProvider<"4,5">

type N5 = numbers.N5
type N4 = numbers.N4

let modN<'n when 'n :> INum and 'n : (new : unit -> 'n)> x =
    Mod<'n>.create x

let modF<'p when 'p :> IPrime and 'p : (new : unit -> 'p)> x =
    ModF<'p>.create x

// be nice and work this out please
printfn "4 + 3 * 2 = %A" (modN 4 + modN 3 * modN 2 : Mod<N5>)

// ho ho ho ... divide like rudolph
printfn "4 / 3 = %A" (modF 4 / modF 3 : ModF<N5>)

// if you are naughty uncomment this line:
// printfn "4 / 3 = %A" (modF 4 / modF 3 : ModF<N4>)
