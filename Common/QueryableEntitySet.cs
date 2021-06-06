using System;
using System.Collections.Generic;
namespace BikeCommon
{
    [Serializable]
    public class QueryableEntitySet<T> where T : IQueryableEntity
    {
        public List<T> Entities { get; set; }
    }
}
