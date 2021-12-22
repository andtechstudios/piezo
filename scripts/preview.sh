#!/usr/bin/env bash

set -e

root=$(git rev-parse --show-toplevel)
cd $root

mkdir -p public
cp -r site/* public/

scripts/frontmatter.csx public/content
scripts/home.csx public/content
hugo-obsidian -input=public/content -output=public/data -index=true 1> /dev/null
scripts/list.csx public/content
hugo serve --source public
