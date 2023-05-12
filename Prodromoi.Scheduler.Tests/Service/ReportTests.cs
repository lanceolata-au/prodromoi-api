using FluentAssertions;
using Prodromoi.Architecture.Tests;
using Prodromoi.Core.Extensions;

namespace Prodromoi.Scheduler.Tests.Service;

public class ReportTests : TestWithDi
{
    [Test]
    public void CanGetAttendanceReportToPdfBytes()
    {
        var operation = () =>
        {
            var htmlReport = File.ReadAllText("./ReportTemplates/AttendanceReport.html");

            var pdfBytes = htmlReport.ConvertHtmlToPdf();
        
            File.WriteAllBytes("./TestReport.pdf", pdfBytes.ToArray());
        };

        operation.Should().NotThrow();

    }
}