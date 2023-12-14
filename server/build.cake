#tool nuget:?package=NuGet.CommandLine&version=5.9.1

var target = Argument("target", "Default");
var buildConfiguration = Argument("configuration", "Release");
var clientProjectPath = "./QRisto.Presentation/ClientApp";

Task("Clean")
    .Does(() =>
{
    CleanDirectory($"./{buildConfiguration}");
});

Task("Restore-NuGet-Packages")
    .Does(() =>
{
    NuGetRestore("./server.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    MSBuild("./server.sln", settings =>
        settings.SetConfiguration(buildConfiguration));
});

Task("Prepare-Client-Dev")
    .Does(() =>
{
    StartProcess("cmd", new ProcessSettings 
    {
        Arguments = "/c npm install",
        WorkingDirectory = clientProjectPath
    });

    StartProcess("cmd", new ProcessSettings 
    {
        Arguments = "/c start-npm.bat",
        WorkingDirectory = clientProjectPath
    });
});

Task("Build-Client")
    .Does(() =>
{
    StartProcess("cmd", new ProcessSettings 
    {
        Arguments = "/c npm install",
        WorkingDirectory = clientProjectPath
    });

    StartProcess("cmd", new ProcessSettings 
    {
        Arguments = "/c npm run build",
        WorkingDirectory = clientProjectPath
    });
});

Task("RunDev")
    .IsDependentOn("Build")
    .IsDependentOn("Prepare-Client-Dev")
    .Does(() =>
{
    var projectPath = "./QRisto.Presentation";

    StartProcess("dotnet", new ProcessSettings 
    {
        Arguments = $"run --project {projectPath} --launch-profile \"Kestrel Development\"",
        WorkingDirectory = System.IO.Path.GetDirectoryName(projectPath)
    });
});

Task("RunProd")
    .IsDependentOn("Build")
    .IsDependentOn("Build-Client")
    .Does(() =>
{
    var projectPath = "./QRisto.Presentation";

    StartProcess("dotnet", new ProcessSettings 
    {
        Arguments = $"run --project {projectPath} --launch-profile \"Kestrel Production\"",
        WorkingDirectory = System.IO.Path.GetDirectoryName(projectPath)
    });
});

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Build")
    .IsDependentOn("RunDev");

RunTarget(target);
