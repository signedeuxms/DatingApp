using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Interfaces
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string passWord);
         Task<User> Login(string userName, string passWord);
         Task<bool> UserExists(string userName);
    }
}