#!/bin/bash

set -eu -o pipefail

shopt -s extglob

PROJECT_JSON="MaxMind.MinFraud/project.json"


VERSION=$(perl -MFile::Slurp::Tiny=read_file -MDateTime <<EOF
use v5.16;
my \$today = DateTime->now->ymd;
my \$log = read_file(q{releasenotes.md});
\$log =~ /\n(\d+\.\d+\.\d+(?:-\w+)?) \((\d{4}-\d{2}-\d{2})\)\n/;
die "Release time is not today! Release: \$2 Today: \$today"
    unless \$today eq \$2;
say \$1;
EOF
)

TAG="v$VERSION"


if [ -n "$(git status --porcelain)" ]; then
    echo ". is not clean." >&2
    exit 1
fi

jq ".version=\"$VERSION\"" "$PROJECT_JSON"| sponge "$PROJECT_JSON"

git diff

read -e -p "Continue (and commit above)? " SHOULD_COMMIT

if [ "$SHOULD_COMMIT" != "y" ]; then
    echo "Aborting"
    exit 1
fi

if [ -n "$(git status --porcelain)" ]; then
    git add "$PROJECT_JSON"
    git commit -m "Prepare for $VERSION"
fi

pushd MaxMind.MinFraud

dotnet restore
dotnet build -c Release
dotnet pack -c Release

popd

pushd MaxMind.MinFraud.UnitTest

dotnet restore
dotnet build
# Disabled until the NUnit test runner works better on Linux
# dotnet run -c Release

popd

read -e -p "Continue given test results? " SHOULD_CONTINUE

if [ "$SHOULD_CONTINUE" != "y" ]; then
    echo "Aborting"
    exit 1
fi

if [ ! -d .gh-pages ]; then
    echo "Checking out gh-pages in .gh-pages"
    git clone -b gh-pages git@github.com:maxmind/minfraud-api-dotnet.git .gh-pages
    pushd .gh-pages
else
    echo "Updating .gh-pages"
    pushd .gh-pages
    git pull
fi

if [ -n "$(git status --porcelain)" ]; then
    echo ".gh-pages is not clean" >&2
    exit 1
fi

popd

PAGE=.gh-pages/index.md
cat <<EOF > "$PAGE"
---
layout: default
title: MaxMind minFraud Score and Insights .NET API
language: dotnet
version: $TAG
---

EOF

cat README.md >> "$PAGE"

monodocer -assembly:MaxMind.MinFraud/bin/Release/net45/MaxMind.MinFraud.dll -importslashdoc:MaxMind.MinFraud/bin/Release/net45/MaxMind.MinFraud.xml "-path:/tmp/dotnet-$TAG" -pretty
mdoc export-html -o ".gh-pages/doc/$TAG" "/tmp/dotnet-$TAG"

pushd .gh-pages

git add doc/
git commit -m "Updated for $TAG" -a

read -e -p "Push to origin? " SHOULD_PUSH

if [ "$SHOULD_PUSH" != "y" ]; then
    echo "Aborting"
    exit 1
fi

git push

popd
git tag "$TAG"
git push
git push --tags

nuget push "MaxMind.MinFraud/bin/Release/MaxMind.MinFraud.$VERSION.nupkg"
