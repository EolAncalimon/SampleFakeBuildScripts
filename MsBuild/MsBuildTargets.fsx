#I __SOURCE_DIRECTORY__
#load "../Common/CommonVariables.fsx"
#load "MsBuildVariables.fsx"
namespace Sample.FakeScripts.MsBuild
    open Sample.FakeScripts.Common.CommonVariables
    open Sample.FakeScripts.MsBuild.BuildVariables
    open Fake
    module BuildTargets = 


        Target "BuildApplication" (fun _ ->
            let setParams defaults =
                { defaults with
                    Targets = ["Rebuild"]
                    Properties =
                        [
                            "Configuration", if Debug = true then "Debug" else "Release"
                            "RunCodeAnalysis", runSonar.ToString()
                            "CodeAnalysisRuleSet", "AllRules.ruleset"
                            "CodeAnalysisCulture", "en-US"
                        ]
                    }
            build setParams (sprintf "%s.sln" SolutionName)
        )

        Target "PublishWebSites" (fun _ ->
                BuildWebsite publishDirectory (sprintf "%s\\%s\\%s\\%s.csproj" rootDir SourceDirectory ProjectName ProjectName)
        )