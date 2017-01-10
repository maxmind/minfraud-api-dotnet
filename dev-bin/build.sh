#!/usr/bin/env bash

set -exu

pushd "$(dirname "$0")/.."

if [ -n "${DOTNETCORE:-}" ]; then

  echo Using .NET CLI

  dotnet restore

  # Running Unit Tests
  dotnet test -f netcoreapp1.0 -c "$CONFIGURATION" ./MaxMind.MinFraud.UnitTest

else

  echo Using Mono

  pushd mono

  nuget restore

  xbuild /p:Configuration=$CONFIGURATION

  mono packages/NUnit.ConsoleRunner.3.5.0/tools/nunit3-console.exe --where "cat != BreaksMono" ./bin/$CONFIGURATION/MaxMind.MinFraud.UnitTest.dll

  popd
fi

popd
