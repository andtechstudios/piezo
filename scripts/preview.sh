#!/usr/bin/env bash

set -e

root=$(git rev-parse --show-toplevel)
cd $root

cd exampleSite

#../scripts/frontmatter.csx content
../scripts/homepage.csx content
hugo-obsidian -input=content -output=data -index=true 1> /dev/null
../scripts/list.csx content
hugo serve --themesDir ../..
