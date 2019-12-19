#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0

#addin Cake.GitVersioning
#addin Cake.FileHelpers
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
//var version = Argument("module_version", "1.0");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./build") + Directory("posh-kentico");

var version = GitVersioningGetVersion();

// Define Files
var moduleFile = buildDir + File("posh-kentico.psd1");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("BuildRootModule")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var settings = new DotNetCoreBuildSettings
    {
        Configuration = configuration
    };

    DotNetCoreBuild("./src/posh-kentico.sln", settings);
});

Task("BuildPowerShellModule")
	.IsDependentOn("BuildRootModule")
	.Does(() =>
{
	Information("Setting module version number to " + version.SimpleVersion.ToString());
	// Update the Version Number
	ReplaceRegexInFiles(moduleFile, "1\\.0", version.SimpleVersion.ToString());

    if (!version.PublicRelease)
    {
        Information("Test: " + version.PrereleaseVersion);

        ReplaceRegexInFiles(moduleFile, "# Prerelease = ''", version.PrereleaseVersion);
    }
});

Task("Build")
	.IsDependentOn("BuildPowerShellModule");

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    NUnit3("./src/**/bin/" + configuration + "/net472/*.Tests.dll", new NUnit3Settings {
        NoResults = true
        });
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
