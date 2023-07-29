using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Create;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Delete;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Update;
using CRUD.DDD.BackEnd.Net.Application.DTOs;
using CRUD.DDD.BackEnd.Net.Application.Queries.Persona;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD.DDD.BackEnd.Net.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly ISender _sender;

        public PersonasController(ISender sender)
        {
            _sender = sender;
        }

        // GET: api/<PersonasController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPersonaQueryDto>>> GetAllPersona()
        {
            var personas = await _sender.Send(new GetPersonaQuery());

            return Ok(personas);
        }

        // GET api/<PersonasController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPersonaQueryDto?>> GetByIdPersona(int id)
        {
            return await _sender.Send(new GetByIdPersonaQuery(id));
        }

        // POST api/<PersonasController>
        [HttpPost]
        public async Task<ActionResult<CreatePersonaCommandDto>> Post([FromBody] CreatePersonaDto createPersona)
        {
            var result = await _sender.Send(new CreatePersonaCommand(createPersona.NoDocumento, createPersona.Nombres, createPersona.Apellidos));

            return CreatedAtAction("GetByIdPersona", new { id = result.IdPersona }, result);
        }

        // PUT api/<PersonasController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdatePersonaDto updatePersona)
        {
            if (id != updatePersona.IdPersona)
            {
                return BadRequest();
            }

            await _sender.Send(new UpdatePersonaCommand(id, updatePersona.NoDocumento, updatePersona.Nombres, updatePersona.Apellidos));

            return NoContent();
        }

        // DELETE api/<PersonasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sender.Send(new DeletePersonaCommand(id));

            return NoContent();
        }
    }
}
