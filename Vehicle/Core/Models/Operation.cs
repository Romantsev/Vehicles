using System;
using System.Text.Json.Serialization;

namespace Core.Models;

public class Operation
{    
    [JsonPropertyName("operationId")]
    public int OperationId { get; set; }
    
    [JsonPropertyName("person")]
    public string Person { get; set; }
    
    [JsonPropertyName("regAddrKoatuu")]
    public string RegAddrKoatuu { get; set; }
    
    [JsonPropertyName("operationCode")]
    public int OperationCode { get; set; }

    [JsonPropertyName("operationName")]
    public string OperationName { get; set; }

    [JsonPropertyName("dateReg")]
    public DateTime DateReg { get; set; }
    
    [JsonPropertyName("depCode")]
    public string DepCode { get; set; }

    [JsonPropertyName("departmentName")]
    public string DepartmentName { get; set; }

    [JsonPropertyName("brand")]
    public string Brand { get; set; }

    [JsonPropertyName("model")]
    public string Model { get; set; }

    [JsonPropertyName("vin")]
    public string Vin { get; set; }

    [JsonPropertyName("makeYear")]
    public DateTime MakeYear { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; }

    [JsonPropertyName("kind")]
    public string Kind { get; set; }

    [JsonPropertyName("body")]
    public string Body { get; set; }

    [JsonPropertyName("purpose")]
    public string Purpose { get; set; }

    [JsonPropertyName("fuel")]
    public string Fuel { get; set; }

    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }

    [JsonPropertyName("ownWeight")]
    public int OwnWeight { get; set; }

    [JsonPropertyName("totalWeight")]
    public int TotalWeight { get; set; }

    [JsonPropertyName("numberRegNew")]
    public string NumberRegNew { get; set; }
}
