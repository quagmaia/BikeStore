using System;

namespace Domain.Entities.Humans
{
    [Serializable]
    public class Salesperson : Human
    {
        public DateTimeOffset TerminationDate { get; set; }
        public Salesperson Manager { get; set; }
    }
}
