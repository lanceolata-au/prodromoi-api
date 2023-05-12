using Prodromoi.Architecture.Tests;
using Prodromoi.Core.Extensions;

namespace Prodromoi.Scheduler.Tests.Service;

public class ReportTests : TestWithDi
{
    [Test]
    public void CanGetHtmlToPdfBytes()
    {
        var htmlReport = File.ReadAllText("./ReportTemplates/AttendanceReport.html");

        var pdfBytes = htmlReport.ConvertHtmlToPdf();
        
        File.WriteAllBytes("./TestReport.pdf", pdfBytes.ToArray());

    }
}