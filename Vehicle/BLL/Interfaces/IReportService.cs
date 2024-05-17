namespace BLL.Interfaces;

public interface IReportService
{
    byte[] GeneratePdfReport(string html);
}