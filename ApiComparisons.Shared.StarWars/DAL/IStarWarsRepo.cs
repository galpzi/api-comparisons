using ApiComparisons.Shared.StarWars.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiComparisons.Shared.StarWars.DAL
{
    public interface IStarWarsRepo
    {
        Task<Human> GetHumanAsync(Guid id);
        Task<Droid> GetDroidAsync(Guid id);
        Task<Human> AddHumanAsync(Human human);
        Task<Human> DeleteHumanAsync(Human human);
        Task<IEnumerable<Human>> GetHumansAsync();
        Task<IEnumerable<Droid>> GetDroidsAsync();
        Task<IEnumerable<Character>> GetFriendsAsync(Character character);
    }
}
