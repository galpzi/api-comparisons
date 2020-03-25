using System;

namespace ApiComparisons.Shared.DAL
{
    public class Purchase
    {
        public Guid ProductID { get; set; }
        public Guid TransactionID { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public virtual Product Product { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
