#!/usr/bin/env dotnet-script

#r "nuget: Humanizer, 2.13.14"

using System.Linq;
using System.Text.RegularExpressions;

using Humanizer;

var searchRoot = Args[0];
var files = Directory.EnumerateFiles(searchRoot, "*.md", SearchOption.AllDirectories);

foreach (var path in files)
{
	Run(path);
}

void Run(string path)
{
	var content = File.ReadAllText(path);
	if (Regex.IsMatch(content, "^-+", RegexOptions.Singleline))
	{
		Console.WriteLine($"'{path}' already has front matter. Skipping...");
		return;
	}

	var fileName = Path.GetFileNameWithoutExtension(path);
	string name;
	if (fileName == "_index")
	{
		name = Path.GetDirectoryName(path);
		name = $"{name.Humanize(LetterCasing.Title)}/";
	}
	else
	{
		name = fileName.Humanize(LetterCasing.Title);
	}

	var writer = new StringWriter();
	writer.WriteLine("---");
	writer.WriteLine($"title: {name}");
	writer.WriteLine("---");
	writer.Write(content);

	File.WriteAllText(path, writer.ToString());
	
	Console.ForegroundColor = ConsoleColor.Green;
	Console.WriteLine($"Added front matter for '{path}'...");
	Console.ResetColor();
}