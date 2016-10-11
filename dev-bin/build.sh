#!/usr/bin/env bash

cd `dirname $0`/..

if [ -n "$DOTNETCORE" ]; then

  echo Using .NET CLI

  dotnet restore

  # Running Unit Tests
  dotnet test -f netcoreapp1.0 -c $CONFIGURATION ./MaxMind.MinFraud.UnitTest

else

  echo Using Mono

  pushd mono

  nuget restore

  xbuild /p:Configuration=$CONFIGURATION

  mono mono/packages/NUnit.ConsoleRunner.3.4.1/tools/nunit3-console.exe --where "cat != BreaksMono" ./mono/bin/$CONFIGURATION/MaxMind.MinFraud.UnitTest.dll

  popd
fi
