namespace FsAdvent.OnThisDayTypeProvider

open FsAdvent.News
open ProviderImplementation.ProvidedTypes
open FSharp.Core.CompilerServices

[<TypeProvider>]
type OnThisDayProvider (config:TypeProviderConfig) as this = 
   inherit TypeProviderForNamespaces ()

   let getProperties newsItems =
       [for (url, title, description) in newsItems ->
           let property = 
               ProvidedProperty(title, typeof<string>, IsStatic=true,
                  GetterCode=fun _ -> <@@ url @@>)
           property.AddXmlDoc(description)
           property]

   let ns = "FsAdvent"
   let asm = System.Reflection.Assembly.GetExecutingAssembly()
   let today = System.DateTime.Now
   let providedType = ProvidedTypeDefinition(asm, ns, "OnThisDay", Some typeof<obj>)
   do  providedType.AddXmlDoc(today.ToLongDateString())
   do  providedType.AddMembersDelayed(fun () ->             
            (today.Month,today.Day) |> getNewsItems |> getProperties
       )
   do  this.AddNamespace(ns, [providedType])