Define-Step -Name 'Clean Solution' -Target 'build' -Body {
	Call-Program 'msbuild.exe' ..\PointOfSale.sln /t:Clean
}

Define-Step -Name 'Build Solution' -Target 'build' -Body {
	Call-Program 'msbuild.exe' ..\PointOfSale.sln /t:Rebuild /p:Configuration=$BuildConfiguration
}

Define-Step -Name 'Run unit tests' -Target 'build,unittests' -Body {
	. (Require-Module 'psmake.mod.testing')
	
	$report_dir = '..\reports'
	Define-NUnit3Tests 'Unit Tests' "..\PointOfSale.Tests.Unit\bin\$BuildConfiguration\PointOfSale.Tests.Unit.dll" | Run-Tests -Cover -CodeFilter '+[PointOfSale*]* -[*Tests*]*' -ReportDirectory $report_dir | Generate-CoverageSummary | Check-AcceptableCoverage -AcceptableCoverage 90
	
}

Define-Step -Name 'Run bdd tests' -Target 'build,bddtests' -Body {
	. (Require-Module 'psmake.mod.testing')
	
	$report_dir = '..\reports'
	
	Define-NUnit3Tests 'BDD Tests' "..\PointOfSale.Tests.BDD\bin\$BuildConfiguration\PointOfSale.Tests.BDD.dll" | Run-Tests -ReportDirectory $report_dir
	
	New-Item -Name "..\BDDReports" -ItemType Directory -Force
	Copy-Item "..\PointOfSale.Tests.BDD\bin\$BuildConfiguration\Reports\*" -Destination "..\BDDReports\" -Recurse
}
