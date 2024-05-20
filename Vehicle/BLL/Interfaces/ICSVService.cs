using Core.Models;

namespace BLL.Interfaces;

public interface ICSVService
{
    void ExportToCSV(List<Operation> operations, string filePath);
    List<Operation> ImportFromCSV(string filePath);
}
