using System.Linq.Expressions;
using Core.Models;

namespace BLL.Interfaces;

public interface IOperationService
{
    void AddOperation(Operation operation);
    void UpdateOperation(Operation operation);
    void DeleteOperation(int operationId);
    Operation GetOperationById(int operationId);
    List<Operation> GetByPredicate(Expression<Func<Operation, bool>> filter = null, 
        Expression<Func<IQueryable<Operation>, IOrderedQueryable<Operation>>> orderBy = null);
    void ClearAllOperations();
}