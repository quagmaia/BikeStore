using BikeCommon;

namespace Persistence.Events
{
    public class ReadWriteEvent<T> where T : IQueryableEntity
    {
        public T Entity { get; set; }
        public ReadWriteAction Action { get; set; }
    }
}
