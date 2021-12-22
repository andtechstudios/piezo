#!/usr/bin/env dotnet-script

#load "core.csx"

var folders = new string[] {
	"posts",
	"projects",
	"notes",
};

var searchRoot = Args[0];
var indexFile = Path.Combine(searchRoot, "_index.md");

var templatePath = Path.Combine(GetScriptDirectory(), "_index.md");

var writer = OpenWriter();
foreach (var line in File.ReadAllLines(templatePath))
{
	writer.WriteLine(line);
}

HTML.BeginList();
foreach (var folder in folders)
{
	HTML.ListItem($"{folder}/", $"/{folder}/", true);
}
HTML.EndList();

File.WriteAllText(indexFile, writer.ToString());
CloseWriter();