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
            LoadData();
        }

        public void Add(Operation item)
        {
            _items.Add(item);
            SaveChanges();
        }

        public void AddRange(List<Operation> items)
        {
            _items.AddRange(items);
            SaveChanges();
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
            
            SaveChanges();
        }

        public void Delete(int itemId)
        {
            var item = _items.Find(i => i.OperationId == itemId);
            if (item == null) return;
            _items.Remove(item);
            SaveChanges();
        }

        public Operation? FindById(int id)
        {
            return _items.Find(i => i.OperationId == id);
        }
        
        public void RemoveAll()
        {
            _items.Clear();
            SaveChanges();
        }
        
        public void SaveChanges()
        {
            SaveData();
        }

        public List<Operation> GetAllAsList()
        {
            return _items;
        }

        private void LoadData()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _items = JsonConvert.DeserializeObject<List<Operation>>(json);
            }
            else
            {
                _items = new List<Operation>();
            }
        }

        private void SaveData()
        {
            var json = JsonConvert.SerializeObject(_items, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}