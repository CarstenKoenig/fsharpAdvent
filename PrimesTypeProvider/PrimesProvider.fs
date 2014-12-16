namespace PrimesTypeProvider

open System
open System.Reflection
open ProviderImplementation.ProvidedTypes
open Microsoft.FSharp.Core.CompilerServices

[<TypeProvider>]
type PrimesProvider (config : TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces ()

    let ns = "Numbers"
    let asm = Assembly.GetExecutingAssembly()

    let createTypes () =
        let n5        = ProvidedTypeDefinition(asm, ns, "N5", Some typeof<obj>)
        let valueProp = ProvidedProperty("Value", typeof<int>, IsStatic = true,
                                        GetterCode = (fun args -> <@@ 5 @@>))
        n5.AddMember(valueProp)
        [n5]

    do
        this.AddNamespace(ns, createTypes())

[<assembly:TypeProviderAssembly>]
do ()