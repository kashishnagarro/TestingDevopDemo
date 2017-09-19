var target = Argument("target", "Default");

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
	
Task("Default")  
    .IsDependentOn("IntegrationTest");

RunTarget(target);