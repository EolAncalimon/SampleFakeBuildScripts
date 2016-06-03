#I __SOURCE_DIRECTORY__
#load "../Common/CommonVariables.fsx"
namespace Sample.FakeScripts.MsBuild
    module BuildVariables =
        open Sample.FakeScripts.Common.CommonVariables
        open Fake

        //parameters
        let SolutionName = getBuildParamOrDefault "solutionName" ""