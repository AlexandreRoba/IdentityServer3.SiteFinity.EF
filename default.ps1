properties {
	$base_directory = Resolve-Path . 
	$src_directory = "$base_directory\source"
	$output_directory = "$base_directory\build"
	$dist_directory = "$base_directory\distribution"
	$sln_file = "$src_directory\IdentityServer3.SiteFinity.EF.sln"
	$target_config = "Release"
	$framework_version = "v4.5"
	$nunit_path = "$src_directory\packages\NUnit.Runners.2.6.4\tools\nunit.exe"
	$nuget_path = "$src_directory\packages\NuGet.CommandLine.2.8.5\tools\nuget.exe"
	
	$buildNumber = 0;
	$version = "0.0.2.0"
	$preRelease = $null
}

task default -depends Clean, CreateNuGetPackage
task appVeyor -depends Clean, CreateNuGetPackage

task Clean {
	rmdir $output_directory -ea SilentlyContinue -recurse
	rmdir $dist_directory -ea SilentlyContinue -recurse
	exec { msbuild /nologo /verbosity:quiet $sln_file /p:Configuration=$target_config /t:Clean }
}

task Compile -depends UpdateVersion {
	exec { msbuild /nologo /verbosity:q $sln_file /p:Configuration=$target_config /p:TargetFrameworkVersion=v4.5 }
}

task UpdateVersion {
	$vSplit = $version.Split('.')
	if($vSplit.Length -ne 4)
	{
		throw "Version number is invalid. Must be in the form of 0.0.0.0"
	}
	$major = $vSplit[0]
	$minor = $vSplit[1]
	$patch = $vSplit[2]
	$assemblyFileVersion =  "$major.$minor.$patch.$buildNumber"
	$assemblyVersion = "$major.$minor.0.0"
	$versionAssemblyInfoFile = "$src_directory/VersionAssemblyInfo.cs"
	"using System.Reflection;" > $versionAssemblyInfoFile
	"" >> $versionAssemblyInfoFile
	"[assembly: AssemblyVersion(""$assemblyVersion"")]" >> $versionAssemblyInfoFile
	"[assembly: AssemblyFileVersion(""$assemblyFileVersion"")]" >> $versionAssemblyInfoFile
}

task RunTests -depends Compile {
	$project = "IdentityServer3SiteFinity.EF.Tests"
	mkdir $output_directory\nunit\$project -ea SilentlyContinue
	.$nunit_path "$output_directory\$project.dll" /html "$output_directory\nunit\$project\index.html"
}

task CreateNuGetPackage -depends Compile {
	$vSplit = $version.Split('.')
	if($vSplit.Length -ne 4)
	{
		throw "Version number is invalid. Must be in the form of 0.0.0.0"
	}
	$major = $vSplit[0]
	$minor = $vSplit[1]
	$patch = $vSplit[2]
	$packageVersion =  "$major.$minor.$patch"
	if($preRelease){
		$packageVersion = "$packageVersion-$preRelease" 
	}

	if ($buildNumber -ne 0){
		$packageVersion = $packageVersion + "-build" + $buildNumber.ToString().PadLeft(5,'0')
	}

  md $dist_directory

	exec { . $nuget_path pack $src_directory\SiteFinityPluginEF\SiteFinityPluginEF.csproj -o $dist_directory -version $packageVersion }
}
