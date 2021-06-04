using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Salesperson : Human
    {
        public DateTimeOffset TerminationDate { get; set; }
        public Salesperson Manager { get; set; }
    }
}
