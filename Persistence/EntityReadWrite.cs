using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Persistence
{
    public class EntityReadWrite<T> where T : IQueryableEntity
    {
        public Dictionary<Guid,T> Entities { get; private set; }

        public readonly FileNames FileName;

        public EntityReadWrite(FileNames fileName)
        {
            FileName = fileName;
            Read();
        }

        public Dictionary<Guid, T> Read()
        {
            var str = FileIo.Read(FileName.ToString());
            var deserialized = JsonSerializer.Deserialize(str, typeof(List<T>)) as List<T>;
            Entities = deserialized.ToDictionary(e => e.Id, e => e);
            return Entities;
        }

        private void Write()
        {
            var str = JsonSerializer.Serialize(Entities);
            FileIo.Write(FileName.ToString(), str);
        }

        public void Add(T entity)
        {
            if (Entities.ContainsKey(entity.Id))
            {
                throw new Exception($"Cannot add duplicate {entity.GetType().Name} item of ID {entity.Id}!");
            }

            Entities.Add(entity.Id, entity);
            Write();
        }
        
        public void Update(T entity)
        {
            if (!Entities.ContainsKey(entity.Id))
            {
                Add(entity);
            }
            Entities[entity.Id] = entity;
            Write();
        }
    }

    public enum FileNames
    {
        Customers,
        Salespeople,
        Discounts,
        Products,
        Sales
    }
}
