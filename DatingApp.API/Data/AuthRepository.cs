using System.Threading.Tasks;
using DatingApp.API.Interfaces;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            this._context = context;

        }


        public async Task<User> Register(User user, string passWord)
        {
            byte[] passWordHash, passWordSalt;
            this.CreatePassWordHash(passWord, out passWordHash, out passWordSalt);
            user.PasswordHash = passWordHash;
            user.PasswordSalt = passWordSalt;

            await this._context.Users.AddAsync(user);
            await this._context.SaveChangesAsync();

            return user;
        }


        public async Task<User> Login(string userName, string passWord)
        {
            var user = await this._context.Users.FirstOrDefaultAsync(
                x => x.UserName == userName
            );
            
            if(user == null){
                return null;
            }

            if( !this.VerifyPassWordHash(passWord, user.PasswordHash, 
                            user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        public async Task<bool> UserExists(string userName)
        {
            if(await this._context.Users.AnyAsync( x => x.UserName == userName) ){
                return true;
            }
            return false;
        }


        private void CreatePassWordHash(string passWord, out byte[] passWordHash, 
                                            out byte[] passWordSalt)
        {
            using(var hash = new System.Security.Cryptography.HMACSHA512())
            {
                passWordSalt = hash.Key;
                passWordHash = hash.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(passWord));
            }            
        }


        private bool VerifyPassWordHash( string passWord, byte[] passWordHash, 
                            byte[] passWordSalt)
        {
            using(var hash = new System.Security.Cryptography.HMACSHA512(passWordSalt))
            {
                var computedHash = hash.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(passWord));

                return this.CompareHash(computedHash, passWordHash);
            } 
        }


        private bool CompareHash(byte[] computedHash, byte[] passWordHash){
            for( int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != passWordHash[i]) return false;
            }
            return true;
        }

    }
}