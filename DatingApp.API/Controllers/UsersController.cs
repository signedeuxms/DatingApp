using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Interfaces;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Dtos;
using System.Collections.Generic;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly IDatingRepository _iDatingRepository;
        private readonly IMapper _mapper;
        public UsersController(IDatingRepository iDatingRepository,
                               IMapper mapper)
        {
            this._iDatingRepository = iDatingRepository;
            this._mapper = mapper; 
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await this._iDatingRepository.GetUsers();
            var usersDto = this._mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(usersDto);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await this._iDatingRepository.GetUser(id);
            var userDto = this._mapper.Map<UserForDetailedDto>(user);

            return Ok(userDto);
        }
    }
}