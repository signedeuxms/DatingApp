using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatingApp.API.Controllers
{
    // http:localhost:5000/api/values
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;

        public ValuesController(DataContext context){
            _context = context;
        }


        // GEt api/values
        /*[HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //throw new Exception("Text exception as message");
            return new string[] {"value1", "value3"};
        }*/


        // GEt api/values
        /*[HttpGet]
        public IActionResult GetValues()
        {
            var values = _context.Values.ToList();
            return Ok(values);
        }*/


        // GEt api/values
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await _context.Values.ToListAsync();
            return Ok(values);
        }


        // GEt api/values/5
        /*[HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }*/


        // GEt api/values/5
        /*[HttpGet("{id}")]
        public IActionResult GetValue(int id)
        {
           var value = _context.Values.FirstOrDefault(x => x.Id == id);
           return Ok(value);
        }*/


        // GEt api/values/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
           var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
           return Ok(value);
        }


        // POST api/values, create record
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5, add record
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // POST api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
