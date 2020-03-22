using System;
using System.Collections.Generic;

namespace ApiComparisons.Shared.DAL
{
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
