using System;
using System.Collections.Generic;

namespace ApiComparisons.Shared.StarWars.Models
{
    public abstract class Character
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public ICollection<Guid> Friends { get; set; }
        //public ICollection<int> AppearsIn { get; set; }
    }
}
