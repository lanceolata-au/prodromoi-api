using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Prodromoi.Core.Features;

public static class ExceptionManager
{
    public static void LogException(this Exception exception)
    {
        Log.Error("Error caught {Message} \n {StackTrace}", 
            exception.Message,
            exception.StackTrace);
        
        switch (exception)
        {
            case AggregateException aggregateException:
                aggregateException.ProcessAggregateException();
                break;
            
            case DbUpdateException dbUpdateException:
                dbUpdateException.ProcessDbUpdateException();
                break;
        }
        
    }

    private static void ProcessAggregateException(this AggregateException exception)
    {
        foreach (var inner in exception.InnerExceptions)
        {
            Log.Error(
                "Error caught {Message} \n {StackTrace}", 
                inner.Message,
                inner.StackTrace);
            inner.LogException();
        }
    }

    public static void ProcessDbUpdateException(this DbUpdateException exception)
    {
        Log.Error(
            "Error caught {Message} \n {StackTrace}", 
            exception.InnerException?.Message,
            exception.InnerException?.StackTrace);
    }
    
    
}