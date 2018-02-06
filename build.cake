//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
var target = Argument("Target", "Default");
var configuration = Argument("Configuration", "Debug");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////
var solutionDir = Directory("./src");
var solutionFile = solutionDir + File("ProChain.sln");
var buildDir = Directory("bin") + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
	.Does(() =>
{
	DotNetCoreClean(solutionFile);
});

Task("Restore-NuGet-Packages")
	.IsDependentOn("Clean")
	.Does(() =>
{
	NuGetRestore(solutionFile);
});

Task("Test")
     .Does(() =>
 {
     var settings = new DotNetCoreTestSettings
     {
         Configuration = configuration
     };

     var projectFiles = GetFiles("./test/**/*.csproj");

     foreach(var file in projectFiles)
     {
         DotNetCoreTest(file.FullPath, settings);
     }
 });

Task("Build")
	.IsDependentOn("Clean")
	.IsDependentOn("Restore-NuGet-Packages")
	.Does(() =>
{
	DotNetCoreBuild(solutionFile, new DotNetCoreBuildSettings()
								{
									Configuration = configuration
								});
});

Task("Default")
	.IsDependentOn("Build")
	.IsDependentOn("Test");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);