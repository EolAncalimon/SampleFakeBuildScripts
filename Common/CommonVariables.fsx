#I __SOURCE_DIRECTORY__
#I "../../FAKE/tools"
#r "FakeLib.dll"
namespace Sample.FakeScripts.Common
    module CommonVariables =
        open Fake

        //Project Folder Structures
        let rootDir = (directoryInfo ".\\").FullName
        let outputDir = rootDir + "build\\output"
        let artifactDir = outputDir + "\\artifacts\\"
        let testResultDir = outputDir + "\\tests\\"
        let packagingRoot = outputDir + "\\deploy\\"
        let publishDirectory = outputDir + "\\publish"
        let libRoot = "lib\\"
        let utilRoot = "utils\\"
        let SourceDirectory = "src"

        //Parameters
        let runSonar = System.Convert.ToBoolean(getBuildParamOrDefault "runSonar" "false")
        let gitSha = getBuildParamOrDefault "GIT_COMMIT" ""
        let ProjectName = getBuildParamOrDefault "projectName" "Optimus-NewProject"
        let Debug = System.Convert.ToBoolean(getBuildParamOrDefault "debug" "true")

        //Build Server Variables
        let version =
            match buildServer with
            | Jenkins -> buildVersion
            | _ -> "6.22.0"

        let buildNumber = 
            match buildServer with
            | Jenkins -> System.Convert.ToInt32(jenkinsBuildNumber)
            | _ -> 1

        //File outputs from tests
        let testResultFile = testResultDir + "TestResults.xml"
        let codeInspectFile = outputDir + "\\Resharper\\InspectCode.xml"
        let codeCoverageFile = testResultDir + "opencoverresults.xml"




