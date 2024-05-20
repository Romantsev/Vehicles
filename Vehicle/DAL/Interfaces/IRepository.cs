using Core.Models;

namespace DAL.Interfaces;

public interface IRepository
{
    List<Operation> GetAllAsList();
    void Add(Operation item);
    void AddRange(List<Operation> items);
    void Edit(Operation item);
    void Delete(int itemId);
    Operation? FindById(int id);
    void RemoveAll();
    Task SaveChangesAsync();
}
