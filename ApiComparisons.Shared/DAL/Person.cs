using System;
using System.Collections.Generic;

namespace ApiComparisons.Shared.DAL
{
    public class Person
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
