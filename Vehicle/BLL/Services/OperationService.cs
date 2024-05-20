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