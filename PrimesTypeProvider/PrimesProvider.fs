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

    let provider =
        let primesProvider = ProvidedTypeDefinition(asm, ns, "PrimesProvider", Some(typeof<obj>))
        let parameters = [ProvidedStaticParameter("Numbers", typeof<string>)]
        primesProvider.DefineStaticParameters (
            parameters, 
            (fun typeName args ->
                let numbers =
                    args.[0] :?> string
                    |> (fun s -> s.Split([|','; ';'|]))
                    |> List.ofArray
                    |> List.map Int32.Parse

                let provider = ProvidedTypeDefinition(asm, ns, typeName, Some typeof<obj>, HideObjectMethods = true)

                let createType (n : int) =
                    let delay () =
                        let name = sprintf "N%d" n

                        let num       = ProvidedTypeDefinition(name, Some typeof<obj>)
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
                    provider.AddMemberDelayed delay

                numbers |> Seq.iter createType
                provider
            ))
        primesProvider

    do
        this.AddNamespace(ns, [inum; iprime; provider])

[<assembly:TypeProviderAssembly>]
do ()