using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Create;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Delete;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Update;
using CRUD.DDD.BackEnd.Net.Application.Queries.Persona;

namespace CRUD.DDD.BackEnd.Net.Application.Services.Persona
{
    public interface IPersonaService
    {
        Task<List<GetPersonaQueryDto>> GetAllAsync();

        Task<GetPersonaQueryDto> GetByIdAsync(GetByIdPersonaQuery getByIdPersona);

        Task<PersonaCommandDto> AddAsync(CreatePersonaCommand createPersona);

        Task<bool> UpdateAsync(UpdatePersonaCommand updatePersona);

        Task<bool> DeleteAsync(DeletePersonaCommand deletePersona);
    }
}
