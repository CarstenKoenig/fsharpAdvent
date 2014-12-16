namespace PrimesTypeProvider

open System
open System.Reflection
open ProviderImplementation.ProvidedTypes
open Microsoft.FSharp.Core.CompilerServices

[<Interface>]
type INum =
    abstract Value : int

[<Interface>]
type IPrime =
    inherit INum

[<TypeProvider>]
type PrimesProvider (config : TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces ()

    let isPrime n =
        if n = 1 then false else
        [2 .. n-1]
        |> Seq.takeWhile (fun d -> d*d <= n)
        |> Seq.forall    (fun d -> n % d <> 0)

    let ns = "Numbers"
    let asm = Assembly.GetExecutingAssembly()

    let inum      = ProvidedTypeDefinition(asm, ns, "INum", Some typeof<INum>)
    let iprime    = ProvidedTypeDefinition(asm, ns, "IPrime", Some typeof<IPrime>)

    let createType (n : int) =
        let name = sprintf "N%d" n

        let num       = ProvidedTypeDefinition(asm, ns, name, Some typeof<obj>)
        let valueProp = ProvidedProperty("Value", typeof<int>, IsStatic = false,
                                        GetterCode = (fun args -> <@@ n @@>))
        let ctor      = ProvidedConstructor([], InvokeCode = fun args -> <@@ () :> obj @@>)

        num.AddMemberDelayed(fun () -> valueProp)
        num.AddMemberDelayed(fun () -> ctor)

        let interfaces () =
            if isPrime n 
            then [inum :> Type; iprime :> Type]
            else [inum :> Type]

        num.AddInterfaceImplementationsDelayed(interfaces)

        num

    let createTypes (upTo : int) =

        let numbers =
            [1 .. upTo]
            |> List.map createType

        [inum; iprime] @ numbers

    do
        this.AddNamespace(ns, createTypes(100))

[<assembly:TypeProviderAssembly>]
do ()