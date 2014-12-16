namespace PrimesTypeProvider

open ProviderImplementation.ProvidedTypes
open Microsoft.FSharp.Core.CompilerServices

[<TypeProvider>]
type PrimesProvider (config : TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces ()

[<assembly:TypeProviderAssembly>]
do ()