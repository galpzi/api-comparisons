using System;

namespace ApiComparisons.Shared.DAL
{
    public class Product
    {
        public Guid ID { get; set; }
        public Guid StoreID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public virtual Store Store { get; set; }
    }
}
