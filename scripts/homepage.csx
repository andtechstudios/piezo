#!/usr/bin/env dotnet-script

#load "core.csx"

var searchRoot = Args[0];
var configDirectory = Path.Combine(Environment.CurrentDirectory, ".piezo");

// Read configuration
var configuration = Configuration.Read(Path.Combine(configDirectory, "config.yaml"));
var templatePath = Path.Combine(configDirectory, "_index.md");

var indexFile = Path.Combine(searchRoot, "_index.md");
var writer = OpenWriter();
foreach (var line in File.ReadAllLines(templatePath))
{
	writer.WriteLine(line);
}

HTML.BeginList();
foreach (var section in configuration.sections)
{
	HTML.ListItem($"{section.name}", $"{section.url}", true);
}
HTML.EndList();

File.WriteAllText(indexFile, writer.ToString());
CloseWriter();