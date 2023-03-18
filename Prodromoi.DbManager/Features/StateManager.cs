using Prodromoi.DbManager.Features.Sql;
using Serilog;

namespace Prodromoi.DbManager.Features;

public class StateManager
{

    private readonly SqlFunctions _functions;
    private readonly bool _resetDatabase = false;
    
    public StateManager(SqlFunctions functions)
    {
        _functions = functions;
        
        var resetDb 
            = Environment.GetEnvironmentVariable("DB_CLEAN");
        if (resetDb != null) _resetDatabase = bool.Parse(resetDb);
    }

    public void ResolveDatabaseState()
    {
        var checkDbQuery =
            $"select exists(" +
            $"select datname from pg_catalog.pg_database " +
            $"where lower(datname) = lower('{Environment.GetEnvironmentVariable("DB_DATABASE")}'));";
        
        var hasDatabase = _functions
            .RunBooleanQuery(checkDbQuery, false);

        if (hasDatabase && !_resetDatabase)
        {
            Log.Information("Database {DB_DATABASE} exists, ready to run schema update", 
                Environment.GetEnvironmentVariable("DB_DATABASE"));
            return;
        }

        if (hasDatabase && _resetDatabase)
        {
            Log.Information("Found database {DB_DATABASE} and reset requested", 
                Environment.GetEnvironmentVariable("DB_DATABASE"));
            Log.Warning("💣💣 Database reset has been requested, will destroy everything 💣💣");
            _functions
                .RunNonQuery($"drop database {Environment.GetEnvironmentVariable("DB_DATABASE")} with (FORCE);",
                    false);
            hasDatabase = false;
        }

        if (!hasDatabase)
        {
            Log.Information("✨ ✨  New {DB_DATABASE} database will be created  ✨ ✨  ", 
                Environment.GetEnvironmentVariable("DB_DATABASE"));
            _functions
                .RunNonQuery(
                    $"create database {Environment.GetEnvironmentVariable("DB_DATABASE")};",
                    false);
            _functions
                .RunNonQuery("create table " +
                             "__schema_versions(" +
                             "name varchar(128) not null," +
                             "executed timestamp with time zone default current_timestamp)");
        }
        
    }

    public void ResolveSchemaState()
    {
        const string structurePath = "./SQL/01_structure/";

        var files = Directory
            .GetFiles(structurePath)
            .ToList();

        files.Sort();
        
        var scripts = 
            files
                .Select(file => 
                    new SqlScript(structurePath, 
                        file.Replace(structurePath, "")))
                .ToList();

        foreach (var script in scripts)
        {
            _functions.CheckIfScriptRun(script);
            if (!_functions.RunScript(script))
            {
                Log.Fatal(
                    "!!! Exiting NOW !!! Failed to run {script} !!! Exiting NOW !!!", 
                    script.Filename);
                return;
            }
        }
        
    }
    
}