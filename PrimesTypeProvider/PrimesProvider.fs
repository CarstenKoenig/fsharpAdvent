namespace PrimesTypeProvider

open System
open System.Reflection

open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Quotations

open ProviderImplementation.ProvidedTypes

[<InterfaceAttribute>]
type INum =
    interface
    end

[<InterfaceAttribute>]
type IPrime =
    inherit INum


[<AutoOpen>]
module NumberOperations =
    let isPrime n =
        if n = 1 then false else
        [2 .. n-1]
        |> Seq.takeWhile (fun d -> d*d <= n)
        |> Seq.forall    (fun d -> n % d <> 0)

[<TypeProvider>]
type NumbersProvider (config : TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces ()

    let ns  = "Numbers"
    let asm = Assembly.GetExecutingAssembly()

    let tempAsmPath = System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), ".dll")
    let tempAsm     = ProvidedAssembly tempAsmPath

    let addEmptyConstructor (t : ProvidedTypeDefinition) =
        let ctor = ProvidedConstructor []
        ctor.InvokeCode <- (fun _ -> <@@ () @@>)
        t.AddMember ctor
        t

    let numbersProvider = ProvidedTypeDefinition(asm, ns, "NumbersProvider", Some(typeof<obj>), IsErased = false)
    let parameters      = [ ProvidedStaticParameter("Numbers", typeof<string>) ]

    let addValueProperty (numberType : ProvidedTypeDefinition) (n : int) =
        let valueProp = ProvidedProperty("Value", typeof<int>, IsStatic = false,
                                         GetterCode = (fun args -> <@@ n @@>))
        numberType.AddMemberDelayed (fun () -> valueProp)

    let addNumberType (toType : ProvidedTypeDefinition) (n : int) =
        let typeName = sprintf "N%d" n
        let numberType = 
            ProvidedTypeDefinition(typeName, Some typeof<obj>, IsErased = false)
            |> addEmptyConstructor

        if isPrime n
        then numberType.AddInterfaceImplementation typeof<IPrime>
        else numberType.AddInterfaceImplementation typeof<INum>

        addValueProperty numberType n
        toType.AddMemberDelayed (fun () -> numberType)


    do
        numbersProvider.DefineStaticParameters (
            parameters, 
            fun typeName args ->
                let numbers =
                    args.[0] :?> string
                    |> fun s -> s.Split ([|','; ';'|])
                    |> Array.map Int32.Parse

                let templateType = 
                    ProvidedTypeDefinition(asm, ns, typeName, Some typeof<obj>, IsErased = false)
                    |> addEmptyConstructor

                numbers |> Seq.iter (addNumberType templateType)

                tempAsm.AddTypes [templateType]
                templateType
            )

        this.RegisterRuntimeAssemblyLocationAsProbingFolder config
        tempAsm.AddTypes [numbersProvider]
        this.AddNamespace(ns, [numbersProvider])

[<assembly:TypeProviderAssembly>]
do ()