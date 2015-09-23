#!/bin/bash

set -e

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

if [ ! -d .gh-pages ]; then
    echo "Checking out gh-pages in .gh-pages"
    git clone -b gh-pages git@github.com:maxmind/minfraud-api-dotnet.git .gh-pages
    cd .gh-pages
else
    echo "Updating .gh-pages"
    cd .gh-pages
    git pull
fi

if [ -n "$(git status --porcelain)" ]; then
    echo ".gh-pages is not clean" >&2
    exit 1
fi

cd ..

PAGE=.gh-pages/index.md
cat <<EOF > $PAGE
---
layout: default
title: MaxMind minFraud Score and Insights .NET API
language: dotnet
version: $TAG
---

EOF

cat README.md >> $PAGE

nuget restore MaxMind.MinFraud.sln

xbuild /p:TargetFrameworkVersion="v4.5" /property:Configuration=Release
monodocer -assembly:MaxMind.MinFraud/bin/Release/MaxMind.MinFraud.dll -importslashdoc:MaxMind.MinFraud/bin/Release/MaxMind.MinFraud.xml -path:/tmp/mf-dotnet-$TAG -pretty
mdoc export-html -o .gh-pages/doc/$TAG /tmp/mf-dotnet-$TAG

cd .gh-pages

git add doc/
git commit -m "Updated for $TAG" -a

read -e -p "Push to origin? " SHOULD_PUSH

if [ "$SHOULD_PUSH" != "y" ]; then
    echo "Aborting"
    exit 1
fi

git push

cd ..
git tag $TAG
git push
git push --tags
