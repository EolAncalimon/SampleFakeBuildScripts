#I __SOURCE_DIRECTORY__
#r "System.Management.Automation"
#r "System.Xml.Linq.dll"
#load "../Common/CommonVariables.fsx"
namespace Sample.FakeScripts.Nuget
    module NugetFunctions =
        open Fake
        open Fake.NuGetHelper
        open Fake.NuGetVersion
        open System.Management.Automation
        open System.Collections.Generic
        open System.Xml.Linq

        let getVersion (nuspecFile : string) = 
            let xd = XDocument.Load nuspecFile
            let nsSys = xd.Root.GetDefaultNamespace()
            xd.Element(nsSys.GetName("package")).Element(nsSys.GetName("metadata")).Element(nsSys.GetName("version")).Value

        //This function will retrieve the version out of the nuspec file as NuGetHelper.getNuspecProperties will error if the nuspec file has a xml declaration
        let getPackageId (nuspecFile : string) = 
            let xd = XDocument.Load nuspecFile
            let nsSys = xd.Root.GetDefaultNamespace()
            xd.Element(nsSys.GetName("package")).Element(nsSys.GetName("metadata")).Element(nsSys.GetName("id")).Value
