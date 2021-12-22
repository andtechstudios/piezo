#!/usr/bin/env dotnet-script

#load "core.csx"

using System.Linq;
using System.Text.RegularExpressions;

var searchRoot = Args[0];
var files = Directory.EnumerateFiles(searchRoot, "*.md", SearchOption.AllDirectories);
var parents = files.Select(x => Path.GetDirectoryName(x))
	.Distinct();
var expandeds = parents
	.Select(x => Path.GetRelativePath(searchRoot, x))
	.SelectMany(FlattenPath)
	.Distinct()
	.Select(x => Path.Combine(searchRoot, x));

foreach (var path in expandeds)
{
	ProcessDirectory(path);
}

void ProcessDirectory(string root)
{
	var indexFile = Path.Combine(root, "_index.md");
	if (File.Exists(indexFile))
	{
		Console.WriteLine($"'{indexFile}' already exists. Skipping...");
		return;
	}
	
	var parent = Path.GetDirectoryName(root);
	var parentPath = GetCanonicalPath(parent);
	var writer = OpenWriter();

	HTML.FrontMatter(Path.GetFileName(root) + "/", false);
	HTML.BeginList();
	HTML.ListItem("../", parentPath, true);
	var directories = Directory.EnumerateDirectories(root);
	foreach (var directory in directories)
	{
		var name = Path.GetFileName(directory) + "/";
		var path = GetCanonicalPath(directory);
		HTML.ListItem(name, path, true);
	}
	var files = Directory.EnumerateFiles(root);
	foreach (var file in files)
	{
		var name = Path.GetFileNameWithoutExtension(file);
		var path = GetCanonicalPath(file);
		HTML.ListItem(name, path);
	}
	HTML.EndList();

	File.WriteAllText(indexFile, writer.ToString());
	CloseWriter();
	
	Console.ForegroundColor = ConsoleColor.Green;
	Console.WriteLine($"Created '{indexFile}'...");
	Console.ResetColor();
}