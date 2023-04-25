using System.Diagnostics;
using System.Text;

namespace Prodromoi.Core.Extensions;

public static class StringExtensions
{
    public static string PhoneNumberString(this string? input)
    {
        return input == null 
            ? string.Empty 
            : new string(input.Where(char.IsDigit).ToArray());
    }
    
    // Generated using OpenAI ChatGPT assistance 20230426
    // ("I need a wrapper for wkhtmltopdf that will output to a bytestream in C#")
    public static IEnumerable<byte> ConvertHtmlToPdf(this string html, string arguments = "")
    {
        const string defaultArguments = "-q -n --page-size A4 ";
        const string argumentFinalizer = " - -";
        var startInfo = new ProcessStartInfo
        {
            FileName = "wkhtmltopdf",
            Arguments = defaultArguments + arguments + argumentFinalizer,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        
        var process = new Process { StartInfo = startInfo };

        process.Start();

        var standardInput = process.StandardInput;
        var standardOutput = process.StandardOutput;

        var inputWriter = new StreamWriter(standardInput.BaseStream, Encoding.UTF8);
        inputWriter.AutoFlush = true;
        inputWriter.Write(html);
        inputWriter.Dispose();
        
        var stringOutput = standardOutput.ReadToEnd();

        process.WaitForExit(10000);
        return Encoding.ASCII.GetBytes(stringOutput);
    }
    
}