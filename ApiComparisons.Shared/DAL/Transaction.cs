using System;
using System.Collections.Generic;

namespace ApiComparisons.Shared.DAL
{
    public class Transaction
    {
        public Guid ID { get; set; }
        public Guid PersonID { get; set; }
        public decimal Total { get; set; }
        public DateTime Created { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
