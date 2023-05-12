using Prodromoi.Architecture.Tests;
using Prodromoi.Core.Extensions;

namespace Prodromoi.Service.Tests.Service;

public class ReportTests : TestWithDi
{
    [Test]
    public void CanGetHtmlToPdfBytes()
    {
        var htmlReport = File.ReadAllText("./Service/HtmlExamples/TestReport.html");

        var pdfBytes = htmlReport.ConvertHtmlToPdf();
        
        File.WriteAllBytes("./TestReport.pdf", pdfBytes.ToArray());

    }
}