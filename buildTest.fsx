#I __SOURCE_DIRECTORY__
#load "Common/CommonVariables.fsx"
#load "MsBuild/MsBuildTargets.fsx"
#load "Nuget/NugetTargets.fsx"

open Fake
open Sample.FakeScripts.Common.CommonVariables
open Sample.FakeScripts.MsBuild.BuildTargets



Target "Default" DoNothing

"BuildApplication"
    ==> "Default"



RunTargetOrDefault "Default"