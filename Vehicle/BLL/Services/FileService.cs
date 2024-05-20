using System.Globalization;
using BLL.Interfaces;
using Core.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace BLL.Services;
public class FileService : IFileService
{
    public void ExportToCSV(List<Operation> operations, string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };

        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, config))
        {
            csv.WriteRecords(operations);
        }
    }

    public void ExportToTXT(List<Operation> operations, string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        {
            foreach (var operation in operations)
            {
                var json = JsonConvert.SerializeObject(operation);
                writer.WriteLine(json);
            }
        }
    }

    public List<Operation> ImportFromCSV(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            return new List<Operation>(csv.GetRecords<Operation>());
        }
    }

    public List<Operation> ImportFromTXT(string filePath)
    {
        var operations = new List<Operation>();

        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var operation = JsonConvert.DeserializeObject<Operation>(line);
                operations.Add(operation);
            }
        }

        return operations;
    }
}
