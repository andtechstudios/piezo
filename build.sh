#!/usr/bin/env bash

set -e

root=$(cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd)
source=$(realpath $1)
destination=$(realpath $2)

$root/scripts/frontmatter.csx $source/content
$root/home.csx $source/content
hugo-obsidian -input=$source/content -output=$source/data -index=true 1> /dev/null
$root/scripts/list.csx site/content
hugo --source $source --destination $destination
