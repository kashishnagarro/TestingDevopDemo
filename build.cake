#tool "nuget:?package=OpenCover"

var target = Argument("target", "Default");

var variableThatStores_GitVersion_FullSemVer = "git version 2.10.2.windows.1";

var directoriesToClean = new []{
    "./GitTeamCityDemo/GitTeamCityDemo/obj",
    "./GitTeamCityDemo/GitTeamCityDemo/bin",
	"./GitTeamCityDemo/IntegrationTest/obj",
    "./GitTeamCityDemo/IntegrationTest/bin",
	"./GitTeamCityDemo/BusinessLayer/obj",
    "./GitTeamCityDemo/BusinessLayer/bin",
	"./GitTeamCityDemo/UnitTestProject/obj",
    "./GitTeamCityDemo/UnitTestProject/bin"
};

Task("Clean")  
    .Does(() =>
{
    CleanDirectories(directoriesToClean);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./GitTeamCityDemo/GitTeamCityDemo.sln", new NuGetRestoreSettings { MSBuildVersion = NuGetMSBuildVersion.MSBuild12 });
});

Task("Build")
  .IsDependentOn("Restore-NuGet-Packages")
  .Does(() =>
{
  MSBuild("./GitTeamCityDemo/GitTeamCityDemo.sln");
});

Task("UnitTest")
	.IsDependentOn("Build")
	.Does(() =>
	{
		MSTest("./GitTeamCityDemo/UnitTestProject/bin/Debug/UnitTestProject.dll");
	});

Task("IntegrationTest")
	.IsDependentOn("UnitTest")
	.Does(() =>
	{
		MSTest("./GitTeamCityDemo/IntegrationTest/bin/Debug/IntegrationTest.dll");
	});

Task("Create-Coverage")
	.IsDependentOn("IntegrationTest")
	.Does(() =>
	{
		OpenCover(tool => {
		  tool.MSTest("./GitTeamCityDemo/UnitTestProject/bin/Debug/UnitTestProject.dll");
		  },
		  new FilePath("./coverage.xml"),
		  new OpenCoverSettings()
			.WithFilter("+[BusinessLayer]*"));
	});
	
Task("Generate-Artifacts")
	.IsDependentOn("Create-Coverage")
	.Does(() =>
	{
		MSBuild("./GitTeamCityDemo/GitTeamCityDemo/GitTeamCityDemo.csproj", new MSBuildSettings()
		  .WithProperty("DeployOnBuild", "true")
		  .WithProperty("WebPublishMethod", "Package")
		  .WithProperty("PackageAsSingleFile", "true")
		  .WithProperty("SkipInvalidConfigurations", "true"));
	});
	
	
Task("Default")  
    .IsDependentOn("Generate-Artifacts");

RunTarget(target);