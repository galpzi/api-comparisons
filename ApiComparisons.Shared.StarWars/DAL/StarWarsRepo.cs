using ApiComparisons.Shared.StarWars.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiComparisons.Shared.StarWars.DAL
{
    public class StarWarsRepo : IStarWarsRepo
    {
        private readonly StarWarsContext context;

        public StarWarsRepo(StarWarsContext context)
        {
            this.context = context;

            Seed(); // TODO: move out of here
        }

        public void Seed()
        {
            this.context.Humans.AddRange(new List<Human>
            {
                new Human
                {
                    Id = Guid.NewGuid(),
                    Name = "Luke Skywalker",
                    //Friends = new[] { "3", "4" },
                    //AppearsIn = new[] { 4, 5, 6 },
                    HomePlanet = "Tatooine"
                },
                new Human
                {
                    Id = Guid.NewGuid(),
                    Name = "Darth Vader",
                    //AppearsIn = new[] { 4, 5, 6 },
                    HomePlanet = "Tatooine"
                }
            });
            this.context.Droids.AddRange(new List<Droid>
            {
                new Droid
                {
                    Id = Guid.NewGuid(),
                    Name = "R2-D2",
                    //Friends = new[] { "1", "4" },
                    //AppearsIn = new[] { 4, 5, 6 },
                    PrimaryFunction = "Astromech"
                },
                new Droid
                {
                    Id = Guid.NewGuid(),
                    Name = "C-3PO",
                    //AppearsIn = new[] { 4, 5, 6 },
                    PrimaryFunction = "Protocol"
                }
            });
            this.context.SaveChanges();
        }

        public async Task<IEnumerable<Character>> GetFriendsAsync(Character character)
        {
            if (character is null)
            {
                return Array.Empty<Character>();
            }

            var friends = new List<Character>();
            // TODO: Replace with context
            //this.context.Humans.Where(o => character.Friends.Contains(o.Id)).Apply(friends.Add);
            //this.context.Droids.Where(o => character.Friends.Contains(o.Id)).Apply(friends.Add);
            return friends;
        }

        public async Task<IEnumerable<Human>> GetHumansAsync()
        {
            return await this.context.Humans.ToListAsync();
        }

        public async Task<IEnumerable<Droid>> GetDroidsAsync()
        {
            return await this.context.Droids.ToListAsync();
        }

        public async Task<Human> GetHumanAsync(Guid id)
        {
            return await this.context.Humans.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Droid> GetDroidAsync(Guid id)
        {
            return await this.context.Droids.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Human> DeleteHumanAsync(Human human)
        {
            Human existing = await this.context.Humans.FirstOrDefaultAsync(o => o.Id == human.Id);
            if (existing is null)
            {
                throw new ArgumentException($"Unable to find human with ID: {human.Id}, name: {human.Name}");
            }
            this.context.Humans.Remove(existing);
            await this.context.SaveChangesAsync();
            return existing;
        }

        public async Task<Human> AddHumanAsync(Human human)
        {
            await this.context.AddAsync(human);
            await this.context.SaveChangesAsync();
            return human;
        }
    }
}
