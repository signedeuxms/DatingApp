using System.Threading.Tasks;
using DatingApp.API.Models;
using System.Collections.Generic;

namespace DatingApp.API.Interfaces
{
    public interface IDatingRepository
    {
        // add a type of User or Photo
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
    }
}