namespace Prodromoi.Core.Dtos;

public class AuditDto
{
    public DateTime Timestamp { get; set; } = DateTime.UnixEpoch;
    public string SourceType { get; set; } = string.Empty;
    public int SourceId { get; set; } = 0;
    public string Actor { get; set; } = string.Empty;
    public string Entry { get; set; } = string.Empty;
}