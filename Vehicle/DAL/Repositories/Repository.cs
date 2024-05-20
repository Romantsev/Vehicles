using DAL.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Core.Models;

namespace DAL.Repositories
{
    public class Repository : IRepository
    {
        private readonly string _filePath;
        private List<Operation> _items;

        public Repository(string filePath)
        {
            _filePath = filePath;
            LoadDataAsync().Wait();
        }

        public void Add(Operation item)
        {
            _items.Add(item);
            SaveChangesAsync().Wait();
        }

        public void AddRange(List<Operation> items)
        {
            _items.AddRange(items);
            SaveChangesAsync().Wait();
        }

        public void Edit(Operation item)
        {
            var existingItem = _items.Find(i => i.OperationId == item.OperationId);

            if (existingItem == null) return;

            existingItem.OperationCode = item.OperationCode;
            existingItem.OperationName = item.OperationName;
            existingItem.DateReg = item.DateReg;
            existingItem.Brand = item.Brand;
            existingItem.Model = item.Model;
            existingItem.MakeYear = item.MakeYear;
            existingItem.Color = item.Color;
            existingItem.Kind = item.Kind;
            existingItem.Body = item.Body;
            existingItem.Purpose = item.Purpose;
            existingItem.Fuel = item.Fuel;
            existingItem.Capacity = item.Capacity;
            existingItem.OwnWeight = item.OwnWeight;
            existingItem.TotalWeight = item.TotalWeight;
            existingItem.NumberRegNew = item.NumberRegNew;
            existingItem.DepCode = item.DepCode;
            existingItem.DepartmentName = item.DepartmentName;
            existingItem.Person = item.Person;
            existingItem.RegAddrKoatuu = item.RegAddrKoatuu;

            SaveChangesAsync().Wait();
        }

        public void Delete(int itemId)
        {
            var item = _items.Find(i => i.OperationId == itemId);
            if (item == null) return;
            _items.Remove(item);
            SaveChangesAsync().Wait();
        }

        public Operation? FindById(int id)
        {
            return _items.Find(i => i.OperationId == id);
        }

        public void RemoveAll()
        {
            _items.Clear();
            SaveChangesAsync().Wait();
        }

        public async Task SaveChangesAsync()
        {
            SaveDataAsync().Wait();
        }

        public List<Operation> GetAllAsList()
        {
            return _items;
        }

        private async Task LoadDataAsync()
        {
            if (File.Exists(_filePath))
            {
                using (var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = new StreamReader(stream))
                {
                    var json = await reader.ReadToEndAsync();
                    _items = JsonConvert.DeserializeObject<List<Operation>>(json);
                }
            }
            else
            {
                _items = new List<Operation>();
            }
        }

        private async Task SaveDataAsync()
        {
            var json = JsonConvert.SerializeObject(_items, Formatting.Indented);
            using (var stream = new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
            using (var writer = new StreamWriter(stream))
            {
                await writer.WriteAsync(json);
            }
        }
    }

}