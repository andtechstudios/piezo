#!/usr/bin/env bash

set -e

buildDir=$(realpath $1)
scriptsDir=$(cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd)
rootDir=$(realpath scriptsDir/..)

cd $rootDir
scripts/frontmatter.csx site/content
scripts/home.csx site/content
hugo-obsidian -input=site/content -output=site/data -index=true 1> /dev/null
scripts/list.csx site/content
hugo --source site --destination $buildDir
