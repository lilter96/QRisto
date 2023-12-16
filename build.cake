#tool nuget:?package=NuGet.CommandLine&version=5.9.1

var target = Argument("target", "Default");
var buildConfiguration = Argument("configuration", "Release");
var clientProjectPath = "./server/QRisto.Presentation/ClientApp";

Task("Clean")
    .Does(() =>
{
    CleanDirectory($"./server/{buildConfiguration}");
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

Task("ClientStart")
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
    .IsDependentOn("ClientStart")
    .Does(() =>
{
    var projectPath = "./server/QRisto.Presentation";

    StartProcess("dotnet", new ProcessSettings 
    {
        Arguments = $"run --project {projectPath} --launch-profile \"Kestrel Development\"",
        WorkingDirectory = "./"
    });
});

Task("RunProd")
    .IsDependentOn("Build")
    .IsDependentOn("Build-Client")
    .Does(() =>
{
    var projectPath = "./server/QRisto.Presentation";

    StartProcess("dotnet", new ProcessSettings 
    {
        Arguments = $"run --project {projectPath} --launch-profile \"Kestrel Production\"",
        WorkingDirectory = "./"
    });
});

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Build")
    .IsDependentOn("RunDev");

RunTarget(target);
