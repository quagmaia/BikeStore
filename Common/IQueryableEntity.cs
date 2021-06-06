using System;
using System.Text.Json.Serialization;

namespace BikeCommon
{
    public interface IQueryableEntity
    {
        public Guid Id { get; set; }
        //public string GuidAsStr { get; set; }
    }

    public abstract class QuertableEntity : IQueryableEntity
    {
        public Guid Id { get; set; }

        public QuertableEntity()
        {
            Id = Guid.NewGuid();
        }

        public QuertableEntity(Guid id)
        {
            Id = id;
        }

    }

}
