using System;
using System.Collections.Generic;

namespace ApiComparisons.Shared.DAL
{
    public class Store
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
