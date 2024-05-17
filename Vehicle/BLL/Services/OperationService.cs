using System.Linq.Expressions;
using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;

namespace BLL.Services;

public class OperationService : IOperationService
{
    private readonly IRepository _repository;

    public OperationService(IRepository repository)
    {
        _repository = repository;
    }
        
    public void AddOperation(Operation operation)
    {
        _repository.Add(operation);
    }

    public void UpdateOperation(Operation operation)
    {
        _repository.Edit(operation);
    }

    public void DeleteOperation(int operationId)
    {
        _repository.Delete(operationId);
    }

    public Operation GetOperationById(int operationId)
    {
        return _repository.FindById(operationId)!;
    }

    public List<Operation> GetByPredicate(
        Expression<Func<Operation, bool>> filter = null, 
        Expression<Func<IQueryable<Operation>, IOrderedQueryable<Operation>>> orderBy = null)
    {
        var query = _repository.GetAllAsList().AsQueryable();
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (orderBy != null)
        {
            query = orderBy.Compile()(query);
        }
        return query.ToList();
    }
    
    public void ExportToTXT(string path)
    {
        var listOfAllOperations = _repository.GetAllAsList();

        using var sw = new StreamWriter(path);
        foreach (var operation in listOfAllOperations)
        {
            sw.WriteLine(operation.OperationCode + " " + operation.OperationName + " " + operation.DateReg + " " + operation.Brand + " " + operation.Model + " " + operation.MakeYear + " " + operation.Color + " " + operation.Kind + " " + operation.Body + " " + operation.Purpose + " " + operation.Fuel + " " + operation.Capacity + " " + operation.OwnWeight + " " + operation.TotalWeight + " " + operation.NumberRegNew + " " + operation.DepCode + " " + operation.DepartmentName + " " + operation.Person + " " + operation.RegAddrKoatuu);
        }
    }
    
    public void ImportFromTXT(string path)
    {
        using var sr = new StreamReader(path);
        while (sr.ReadLine() is { } line)
        {
            var operation = new Operation();
            var values = line.Split(' ');
            operation.OperationCode = int.Parse(values[0]);
            operation.OperationName = values[1];
            operation.DateReg = DateTime.Parse(values[2]);
            operation.Brand = values[3];
            operation.Model = values[4];
            operation.MakeYear = DateTime.Parse(values[5]);
            operation.Color = values[6];
            operation.Kind = values[7];
            operation.Body = values[8];
            operation.Purpose = values[9];
            operation.Fuel = values[10];
            operation.Capacity = int.Parse(values[11]);
            operation.OwnWeight = int.Parse(values[12]);
            operation.TotalWeight = int.Parse(values[13]);
            operation.NumberRegNew = values[14];
            operation.DepCode = values[15];
            operation.DepartmentName = values[16];
            operation.Person = values[17];
            operation.RegAddrKoatuu = values[18];
            _repository.Add(operation);
        }
    }
    
    public void ClearAllOperations()
    {
        _repository.RemoveAll();
    }

    public List<Operation> Search(string search)
    {
        var listOfAllOperations = _repository.GetAllAsList();
        listOfAllOperations = listOfAllOperations.Where(op => op.Brand.Contains(search)).ToList();

        return listOfAllOperations;
    }
}