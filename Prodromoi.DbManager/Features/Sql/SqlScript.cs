namespace Prodromoi.DbManager.Features.Sql;

public class SqlScript
{
    private string Path { get; init; }
    public string Filename { get; init; }

    public string Content => GetScriptContent();
    
    public bool HasBeenRun { get; set; }

    public SqlScript(string path, string filename)
    {
        Path = path;
        Filename = filename;
    }

    private string GetScriptContent()
    {
        return File.ReadAllText($"./{Path}/{Filename}");
    }
    
}