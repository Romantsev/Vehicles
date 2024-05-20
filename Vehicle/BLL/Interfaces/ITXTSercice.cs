using Core.Models;

namespace BLL.Interfaces;

public interface ITXTService
{
    void ExportToTXT(List<Operation> operations, string filePath);
    List<Operation> ImportFromTXT(string filePath);
}
