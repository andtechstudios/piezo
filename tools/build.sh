#!/usr/bin/env bash

set -e

buildDir=$1
scripts/frontmatter.csx public/content
scripts/home.csx public/content
hugo-obsidian -input=public/content -output=public/data -index=true 1> /dev/null
scripts/list.csx public/content
hugo --source site --destination $buildDir
