
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

type numbers = Numbers.NumbersProvider<"4,5">

type N5 = numbers.N5
type N4 = numbers.N4

module Main =

    let modF<'p when 'p :> IPrime and 'p : (new : unit -> 'p)> x =
        ModF<'p>.create x

    [<EntryPoint>]
    let main argv = 

        let x : ModF<N5> = modF 4 / modF 3
        printfn "4 / 3 = %A" x

        0 // return an integer exit code
