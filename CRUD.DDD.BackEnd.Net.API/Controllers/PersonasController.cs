using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Create;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Delete;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Update;
using CRUD.DDD.BackEnd.Net.Application.Queries.Persona;
using CRUD.DDD.BackEnd.Net.Application.Services.Persona;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD.DDD.BackEnd.Net.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaService _personaService;

        public PersonasController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        // GET: api/<PersonasController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPersonaQueryDto>>> GetAllPersona()
        {
            return await _personaService.GetAllAsync();
        }

        // GET api/<PersonasController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPersonaQueryDto>> GetByIdPersona(int id)
        {
            return await _personaService.GetByIdAsync(new GetByIdPersonaQuery(id));
        }

        // POST api/<PersonasController>
        [HttpPost]
        public async Task<ActionResult<PersonaCommandDto>> Post([FromBody] CreatePersonaCommand createPersona)
        {
            var result = await _personaService.AddAsync(createPersona);

            return CreatedAtAction("GetByIdPersona", new { id = result.IdPersona }, result);
        }

        // PUT api/<PersonasController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdatePersonaCommand updatePersona)
        {
            if (id != updatePersona.IdPersona)
            {
                return BadRequest();
            }

            await _personaService.UpdateAsync(updatePersona);

            return NoContent();
        }

        // DELETE api/<PersonasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _personaService.DeleteAsync(new DeletePersonaCommand(id));

            return NoContent();
        }
    }
}
