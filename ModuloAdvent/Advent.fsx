
(*
                                                       *
     *                                                          *
                                  *                  *        .--.
      \/ \/  \/  \/                                        ./   /=*
        \/     \/      *            *                ...  (_____)
         \ ^ ^/                                       \ \_((^o^))-.     *
         (o)(O)--)--------\.                           \   (   ) \  \._.
         |    |  ||================((~~~~~~~~~~~~~~~~~))|   ( )   |     \
          \__/             ,|        \. * * * * * * ./  (~~~~~~~~~~~)    \
   *        ||^||\.____./|| |          \___________/     ~||~~~~|~'\____/ *
            || ||     || || A            ||    ||          ||    |   jurcy
     *      <> <>     <> <>          (___||____||_____)   ((~~~~~|   *

*)

#r @"../PrimesTypeProvider/bin/Debug/PrimesTypeProvider.dll"
#r @"./bin/Debug/ModuloAdvent.exe"

open PrimesTypeProvider
open ModuloAdvent

type numbers = Numbers.NumbersProvider<"4,5">

type N5 = numbers.N5
type N4 = numbers.N4

let modF<'p when 'p :> IPrime and 'p : (new : unit -> 'p)> x =
    ModF<'p>.create x


// ho ho ho ... divide like rudolph
printfn "4 / 3 = %A" (modF 4 / modF 3 : ModF<N5>)

// if you are naughty uncomment this line:
// printfn "4 / 3 = %A" (modF 4 / modF 3 : ModF<N4>)
