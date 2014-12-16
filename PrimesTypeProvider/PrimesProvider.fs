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

    let ns = "Numbers"
    let asm = Assembly.GetExecutingAssembly()

    let createTypes () =

        let inum      = ProvidedTypeDefinition(asm, ns, "INum", Some typeof<INum>)
        let iprime    = ProvidedTypeDefinition(asm, ns, "IPrime", Some typeof<IPrime>)

        let n5        = ProvidedTypeDefinition(asm, ns, "N5", Some typeof<obj>)
        let valueProp = ProvidedProperty("Value", typeof<int>, IsStatic = false,
                                        GetterCode = (fun args -> <@@ 5 @@>))
        let ctor      = ProvidedConstructor([], InvokeCode = fun args -> <@@ () :> obj @@>)

        n5.AddMember(valueProp)
        n5.AddMember(ctor)
        n5.AddInterfaceImplementation(inum)
        n5.AddInterfaceImplementation(iprime)

        [inum; iprime; n5]

    do
        this.AddNamespace(ns, createTypes())

[<assembly:TypeProviderAssembly>]
do ()