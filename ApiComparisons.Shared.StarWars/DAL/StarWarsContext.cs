using ApiComparisons.Shared.StarWars.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiComparisons.Shared.StarWars.DAL
{
    public class StarWarsContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Human> Humans { get; set; }
        public DbSet<Droid> Droids { get; set; }

        public StarWarsContext(DbContextOptions<StarWarsContext> options) : base(options)
        {
        }
    }
}
