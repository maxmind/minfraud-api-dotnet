$ErrorActionPreference = 'Stop'
$DebugPreference = 'Continue'

$projectJsonFile=(Get-Item "MaxMind.MinFraud/project.json").FullName
$matches = (Get-Content -Encoding UTF8 releasenotes.md) ` |
            Select-String '(\d+\.\d+\.\d+(?:-\w+)?) \((\d{4}-\d{2}-\d{2})\)' `

$version = $matches.Matches.Groups[1].Value
$date = $matches.Matches.Groups[2].Value

if((Get-Date -format 'yyyy-MM-dd')  -ne $date ) {
    Write-Error "$date is not today!"
    exit 1
}

$tag = "v$version"

if (& git status --porcelain) {
    Write-Error '. is not clean'
}

# Not using Powershell's built-in JSON support as that
# reformats the file.
(Get-Content -Encoding UTF8 $projectJsonFile) `
    -replace '(?<=version"\s*:\s*")[^"]+', $version ` |
  Out-File -Encoding UTF8 $projectJsonFile


& git diff

if ((Read-Host -Prompt 'Continue? (y/n)') -ne 'y') {
    Write-Error 'Aborting'
}

if (-Not(& git status --porcelain)) {
    & git add $projectJsonFile
    & git commit -m "Prepare for $version"
}

Push-Location MaxMind.MinFraud

& dotnet restore
& dotnet build -c Release
& dotnet pack -c Release

Pop-Location

Push-Location MaxMind.MinFraud.UnitTest

& dotnet restore
& dotnet test -c Release

Pop-Location

if ((Read-Host -Prompt 'Continue given tests? (y/n)') -ne 'y') {
    Write-Error 'Aborting'
}

if (Test-Path .gh-pages ) {
    Write-Debug "Updating .gh-pages"
    Push-Location .gh-pages
    & git pull
} else {
    Write-Debug "Checking out gh-pages in .gh-pages"
    & git clone -b gh-pages git@github.com:maxmind/minfraud-api-dotnet.git .gh-pages
    Push-Location .gh-pages
}

if (& git status --porcelain) {
    Write-Error '.gh-pages is not clean'
}
Pop-Location

$page = '.gh-pages\index.md'

@"
---
layout: default
title: MaxMind minFraud Score and Insights .NET API
language: dotnet
version: $tag
---
"@ | Out-File -Encoding UTF8 -Append $page

Get-Content -Encoding UTF8 'README.md' | Out-File -Encoding UTF8 -Append $page

& MSBuild.exe .\minfraud.shfbproj /p:OutputPath=.gh-pages\doc\$tag

Push-Location .gh-pages

& git add doc/
& git commit -m "Updated for $tag" -a

if ((Read-Host -Prompt 'Should push? (y/n)') -ne 'y') {
    Write-Error 'Aborting'
}

& git push

Pop-Location
& git tag "$tag"
& git push
& git push --tags

& nuget push "MaxMind.MinFraud/bin/Release/MaxMind.MinFraud.$version.nupkg" -Source https://www.nuget.org/api/v2/package
