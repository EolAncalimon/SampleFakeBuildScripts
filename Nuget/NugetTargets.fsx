#I __SOURCE_DIRECTORY__
#load "../Common/CommonVariables.fsx"
#load "NugetVariables.fsx"
#load "NugetFunctions.fsx"
namespace Sample.FakeScripts.Nuget

    module NugetTargets = 
        open Fake
        open Fake.NuGetVersion
        open Sample.FakeScripts.Common.CommonVariables
        open Sample.FakeScripts.Nuget.NugetVariables
        open Sample.FakeScripts.Nuget.NugetFunctions

        Target "PackageRelease" (fun _ ->
                let formattedBuildNumber = if buildNumber < 10 then ("000" + buildNumber.ToString()) elif buildNumber < 100 then ("00" + buildNumber.ToString()) elif buildNumber < 1000 then ("0" + buildNumber.ToString()) else buildNumber.ToString()
                CreateDir packagingRoot
                NuGet(fun p ->
                    { p with
                        Version = version + if Debug = true then ("-prerelease" + formattedBuildNumber) else ""
                        Project = ProjectName
                        OutputPath = packagingRoot
                        WorkingDir = (publishDirectory + "\\" + ProjectName)
                    }) ( sprintf "%s\\%s\\%s.nuspec" SourceDirectory ProjectName ProjectName)
        )

        Target "PackageComponent" (fun _ ->
            //TODO: Change back to 0000 format when the first release has happened
            let formattedBuildNumber = if buildNumber < 10 then ("00" + buildNumber.ToString()) elif buildNumber < 100 then ("0" + buildNumber.ToString()) elif buildNumber < 1000 then ("" + buildNumber.ToString()) else buildNumber.ToString() 
            !!(sprintf "%s\\**\\*.nuspec" SourceDirectory) -- (sprintf "%s\\**\\%s.nuspec" SourceDirectory ProjectName )
                |> Seq.iter(fun f ->
                    printfn "Processing %s" (f)
                    let packageId = getPackageId(f)
                    let nuspecVersion = getVersion(f)
                    printfn "nuspecVersion is %s" (nuspecVersion.ToString())
                    try
                        NuGet(fun p -> 
                            {p with
                                WorkingDir = (fileInfo f).Directory.FullName + "\\"
                                Project = (fileNameWithoutExt f)
                                Properties =
                                    [
                                        "Configuration", if Debug = true then "Debug" else "Release"
                                    ]
                                Version = nuspecVersion + (if Debug = true then ("-build" + formattedBuildNumber) else "")
                            }) ((fileInfo f).FullName.Replace(".nuspec", ".csproj"))
                    with
                    | exn -> 
                        (printfn "Package '%s' failed to pack. Reported error is: %s" (f.ToString()) (exn.Message))
                        traceEndTask "publishPackage" "test"

                )
        )