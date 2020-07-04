using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using DatingApp.API.Models;


namespace DatingApp.API.Data
{
    public class Seed
    {
        public static void SeedUsers( DataContext context )
        {
            if( !context.Users.Any() )
            {
                var userData = System.IO.File.ReadAllText( "Data/UserSeedData.json" );
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                foreach ( var user in users )
                {
                    byte[] passWordHash, passWordSalt;

                    CreatePassWordHash( "password", out passWordHash, out passWordSalt );

                    user.PasswordHash = passWordHash;
                    user.PasswordSalt = passWordSalt;
                    user.UserName = user.UserName.ToLower();

                    context.Users.Add(user);
                }

                context.SaveChanges();
            }
        }


        private static void CreatePassWordHash(string passWord, out byte[] passWordHash, 
                                            out byte[] passWordSalt)
        {
            using(var hash = new System.Security.Cryptography.HMACSHA512())
            {
                passWordSalt = hash.Key;
                passWordHash = hash.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(passWord));
            }            
        }
    }
}