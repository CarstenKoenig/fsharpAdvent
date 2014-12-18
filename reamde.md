
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


## What the type provider is doing:
- it provides two interfaces `INum` and `IPrime` into `.Numbers`
- it provides a *Provider* `PrimesProvider` you can give a list of numbers you are interested in "1,5,13" and returns a type with the prober nested types
- nests those types with `INum` and if it is a prime number with `IPrime` as well.

## how should the numbers be represented

- they should implement `INum` with a single property `Value`
- they should implement `IPrim` if they are prime
- they should have a parameterless constructor


## Steps taken
- Create a F\# lib file for the type provider
- Followed the tutorial
- Right now there was an issue with the Provider files and [FsharpData] - so I just removed the references and choose to log to the console instead
- Implemented a first very simple type `N5` with just a single static property `Value` (of type `int` with value `5`)


## References

A great tutorial on how to build a *type provider* can be found at [Michael Newtons great blog][mvann]


## References
[mvann]: http://blog.mavnn.co.uk/type-providers-from-the-ground-up/ "Michael Newton: Type providers from the ground up"
[FsharpData]: https://fsharp.github.io/FSharp.Data/