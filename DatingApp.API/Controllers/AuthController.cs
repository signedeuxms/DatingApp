using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Interfaces;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace DatingApp.API.Controllers
{
    // http:localhost:5000/api/auth/
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repository, IConfiguration config)
        {
            this._repository = repository;
            this._config = config;
        }


        // http:localhost:5000/api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
        {
            // validate the request

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();

            if( await this._repository.UserExists(userForRegisterDto.UserName))
            {
                return BadRequest("\n userName already exists");
            }

            var userToCreate = new User
            {
                UserName = userForRegisterDto.UserName
            };

            var createdUser = await this._repository.Register(userToCreate, 
                            userForRegisterDto.PassWord);
        
            return StatusCode(201);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForRegisterDto userForRegisterDto)
        {
            var userFromRepo = await this._repository.Login(
                userForRegisterDto.UserName.ToLower(),
                                        userForRegisterDto.PassWord);

            if(userFromRepo == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(this._config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok( new {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}