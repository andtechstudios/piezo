#r "nuget: YamlDotNet, 11.2.1"

using System.IO;
using YamlDotNet.Serialization;

public class Configuration
{
	public List<Location> locations;

	public class Location
	{
		public string name;
		public string url;
	}

	public static Configuration Read(string path)
	{
		var input = new StreamReader(path);
		var deserializer = new DeserializerBuilder()
			.Build();

		return deserializer.Deserialize<Configuration>(input);
	}
}

string GetScriptDirectory() => Path.GetDirectoryName(GetCurrentFilePath());

string GetCurrentFilePath([System.Runtime.CompilerServices.CallerFilePath] string fileName = null) => fileName;

TextWriter OpenWriter()
{
	var writer = new StringWriter();
	Console.SetOut(writer);
	return writer;
}

void CloseWriter()
{
    Console.Out.Close();
    var sw = new StreamWriter(Console.OpenStandardOutput());
	sw.AutoFlush = true;
    Console.SetOut(sw);
}

IEnumerable<string> FlattenPath(string path)
{
	var tokens = path.Split(new char[] { '/', '\\' });
	for (int i = 0; i < tokens.Length; i++)
	{
		yield return Path.Join(tokens.Take(i + 1).ToArray());
	}
}

string GetCanonicalPath(string path)
{
	bool isDirectory = Directory.Exists(path);

	path = Path.GetRelativePath(searchRoot, path);
	if (!isDirectory)
	{
		var directory = Path.GetDirectoryName(path);
		var fileName = Path.GetFileNameWithoutExtension(path);
		path = Path.Combine(directory, fileName);
	}

	if (path == ".")
	{
		return "/";
	}

	return isDirectory ? $"/{path}/" : $"/{path}";
}

public class HTML
{

	public static void FrontMatter(string title, bool toc)
	{
		Console.WriteLine("---");
		Console.WriteLine($"title: {title}");
		Console.WriteLine($"toc: {toc}");
		Console.WriteLine("---");
	}

	public static void BeginList()
	{
		Console.WriteLine("{{< unsafe >}}");
		Console.WriteLine("<div class=\"page-end\">");
		Console.WriteLine("<div class=\"backlinks-container\">");
		Console.WriteLine("<ul id=\"dirlist\" class=\"backlinks\">");
	}

	public static void ListItem(string displayName, string url, bool isFolder = false)
	{
		var value = isFolder ? $"{displayName}" : $"{displayName}";
		Console.WriteLine($"<li><a href=\"{url}\" class=\"diritem\"><div>{value}</div></a></li>");
	}

	public static void EndList()
	{			
		Console.WriteLine("</ul>");
		Console.WriteLine("</div>");
		Console.WriteLine("</div>");
		Console.WriteLine("{{< /unsafe >}}");
	}
}
