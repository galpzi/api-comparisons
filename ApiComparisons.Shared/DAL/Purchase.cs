using System;

namespace ApiComparisons.Shared.DAL
{
    public class Purchase
    {
        public int ProductID { get; set; }
        public int TransactionID { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public virtual Product Product { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
