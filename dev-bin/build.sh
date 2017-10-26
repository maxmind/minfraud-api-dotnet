#!/usr/bin/env bash

set -exu

pushd "$(dirname "$0")/.."

if [ -n "${DOTNETCORE:-}" ]; then

  echo Using .NET CLI

  if [[ "$TRAVIS_OS_NAME" == "osx" ]]; then
    # This is due to: https://github.com/NuGet/Home/issues/2163#issue-135917905
    echo "current ulimit is: `ulimit -n`..."
    ulimit -n 1024
    echo "new limit: `ulimit -n`"
  fi

  dotnet restore ./MaxMind.MinFraud.sln

  # Running Unit Tests
  dotnet test -f "$CONSOLE_FRAMEWORK" -c "$CONFIGURATION" ./MaxMind.MinFraud.UnitTest/MaxMind.MinFraud.UnitTest.csproj

else

  echo Using Mono

  msbuild /t:restore ./MaxMind.MinFraud.sln

  msbuild /t:build /p:Configuration=$CONFIGURATION /p:TargetFramework=net452 ./MaxMind.MinFraud.UnitTest/MaxMind.MinFraud.UnitTest.csproj

  nuget install xunit.runner.console -ExcludeVersion -Version 2.2.0 -OutputDirectory .
  mono ./xunit.runner.console/tools/xunit.console.exe ./MaxMind.MinFraud.UnitTest/bin/$CONFIGURATION/net452/MaxMind.MinFraud.UnitTest.dll

fi

popd
