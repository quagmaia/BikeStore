using BikeCommon;
using Persistence.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Persistence
{
    public interface IEntityReadWrite<T> where T : IQueryableEntity
    {
        public Dictionary<Guid, T> Entities { get; }
        T GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void CommitChanges();
    }

    public class EntityReadWrite<T> : IEntityReadWrite<T> where T : IQueryableEntity
    {
        public Dictionary<Guid, T> Entities { get; private set; } = new Dictionary<Guid, T>();
        public FileNames FileName { get; }

        private readonly Action<ReadWriteEvent<T>> RaiseEvent;

        public EntityReadWrite(FileNames fileName, Action<ReadWriteEvent<T>> raiseEventCallback)
        {
            FileName = fileName;
            RaiseEvent = raiseEventCallback;
            ReadAll();
        }

        public T GetById(Guid id)
        {
            ReadAll();
            var found  = Entities.TryGetValue(id, out var entity);
            return found ? entity : default;
        }

        public void CommitChanges()
        {
            var str = JsonSerializer.Serialize(Entities);
            FileIo.Write(FileName.ToString(), str);

            ReadAll();
        }

        public void Add(T entity)
        {
            if (Entities.ContainsKey(entity.Id))
            {
                throw new Exception($"Cannot add duplicate {entity.GetType().Name} item of ID {entity.Id}!");
            }

            Entities.Add(entity.Id, entity);

            var rwe = new ReadWriteEvent<T>() { Entity = entity, Action = ReadWriteAction.Added };
            RaiseEvent.Invoke(rwe);
        }
                
        public void Update(T entity)
        {
            if (!Entities.ContainsKey(entity.Id))
            {
                Add(entity);
            }
            Entities[entity.Id] = entity;

            var rwe = new ReadWriteEvent<T>() { Entity = entity, Action = ReadWriteAction.Updated };
            RaiseEvent.Invoke(rwe);
        }

        public void Delete(T entity)
        {
            if (Entities.ContainsKey(entity.Id))
            {
                Entities.Remove(entity.Id);
            }

            var rwe = new ReadWriteEvent<T>() { Entity = entity, Action = ReadWriteAction.Deleted };
            RaiseEvent.Invoke(rwe);
        }

        private Dictionary<Guid, T> ReadAll()
        {
            var str = FileIo.Read(FileName.ToString());
            try
            {
                var deserialized = JsonSerializer.Deserialize(str, typeof(List<T>)) as List<T>;
                Entities = deserialized.ToDictionary(e => e.Id, e => e);
            }
            catch (JsonException e)
            {
                Console.WriteLine($"Could not parse {FileName}");
            }
            return Entities;
        }
    }
}
