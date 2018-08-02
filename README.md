# Point Of Sale Terminal

This is a youscan interview task implementation.

### Usage
You can build a project from visual studio 2017, or from powershell build scripts.
Using build scripts you can also check test coverage and run bdd tests to validate correctness of implementation.

To run build scripts you have to navigate to src\build folder in you powershell console and run .\make_local.ps1 script

Note that msbuild.exe should be added to your PATH environment variable to be able to build solution from powershell.

After running BDD tests you can view a results in browser by opening 'src\BDDReports\FeaturesReport.html' file.

Also, you can view unit tests code coverage by opening 'src\reports\summary\index.html'