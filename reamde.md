
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