using System.Transactions;
using Npgsql;
using Serilog;

namespace Prodromoi.DbManager.Features.Sql;

public class SqlFunctions
{
    private SqlConnection Connection { get; set; }
    
    public SqlFunctions(SqlConnection connection)
    {
        Connection = connection;
    }

    public bool RunBooleanQuery(string query, bool usedScoped = true)
    {
        Log.Verbose("Running RunBooleanQuery: {Query}", query);
        
        var result = false;
        
        GetConnection(usedScoped).Open();
        
        var command = new NpgsqlCommand(query, GetConnection(usedScoped));

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            result = reader.GetBoolean(0);
        }
        
        GetConnection(usedScoped).Close();

        return result;
    }


    public void RunNonQuery(string query, bool usedScoped = true)
    {        
        Log.Verbose("Running RunNonQuery: {Query}", query);

        GetConnection(usedScoped).Open();
        
        var command = new NpgsqlCommand(query, GetConnection(usedScoped));

        var result = command.ExecuteNonQuery();

        GetConnection(usedScoped).Close();
    }

    public void CheckIfScriptRun(SqlScript script)
    {
        var query = $"select exists ( select from schema_versions where name = '{script.Filename}' )";
        
        Log.Information("🟦 Checking SQL File -- {Filename} -- ", script.Filename);
        
        script.HasBeenRun = RunBooleanQuery(query);

        if (script.HasBeenRun) Log.Information("🟧 Already Run -- {Filename} -- ", script.Filename);

    }

    public bool RunScript(SqlScript script)
    {
        if (script.HasBeenRun) return true;

        GetConnection().Open();

        var begin = new NpgsqlCommand("begin;", GetConnection());
        begin.ExecuteNonQuery();

        var scriptContent = script.GetScriptContent();
            
        Log.Verbose("Running script with content: \n\n {scriptContent}", scriptContent);
            
        var scriptFunction = new NpgsqlCommand(scriptContent, GetConnection());
        try
        {
            scriptFunction.ExecuteNonQuery();
            
            var commit = new NpgsqlCommand("commit;", GetConnection());
            commit.ExecuteNonQuery();

            var recordCommandString = $"insert into schema_versions values ('{script.Filename}',now())";
            var recordCommand = new NpgsqlCommand(recordCommandString, GetConnection());
            recordCommand.ExecuteNonQuery();

            Log.Information("🟩 Applied to Schema -- {Filename} -- ", script.Filename);
            script.HasBeenRun = true;
        }
        catch (Exception e)
        {
            Log.Error("Failed to run script with following: ");
            
            if (e is PostgresException) Log.Error("{SQLError}", e.Message);
            else Log.Error("{stackTrace}", e.StackTrace);
            
            var rollback = new NpgsqlCommand("rollback;", GetConnection());
            Log.Warning("Rollback triggered");
            rollback.ExecuteNonQuery();
            Log.Information("🟥 Failed Script     -- {Filename} -- ", script.Filename);
        }
        finally
        {
            GetConnection().Close();
        }

        return script.HasBeenRun;

    }
    
    private NpgsqlConnection GetConnection(bool usedScoped = true)
    {
        return usedScoped ? Connection.DatabaseScopedConnection : Connection.DatabaseConnection;
    }
    
}