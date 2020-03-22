using System;
using System.Collections.Generic;

namespace ApiComparisons.Shared.DAL
{
    public class Transaction
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public decimal Total { get; set; }
        public DateTime Created { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
