using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Create;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Delete;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Update;
using CRUD.DDD.BackEnd.Net.Application.Queries.Persona;

namespace CRUD.DDD.BackEnd.Net.Application.Services.Persona
{
    public class PersonaService : IPersonaService
    {
        private readonly CreatePersonaCommandHandler _createPersonaCommandHandler;
        private readonly UpdatePersonaCommandHandler _updatePersonaCommandHandler;
        private readonly DeletePersonaCommandHandler _deletePersonaCommandHandler;
        private readonly GetAllPersonaQueryHandler _getAllPersonaQueryHandler;
        private readonly GetByIdPersonaQueryHandler _getByIdPersonaQueryHandler;

        public PersonaService(
            CreatePersonaCommandHandler createPersonaCommandHandler,
            UpdatePersonaCommandHandler updatePersonaCommandHandler,
            DeletePersonaCommandHandler deletePersonaCommandHandler,
            GetAllPersonaQueryHandler getAllPersonaQueryHandler,
             GetByIdPersonaQueryHandler getByIdPersonaQueryHandler
            )
        {
            _createPersonaCommandHandler = createPersonaCommandHandler;
            _updatePersonaCommandHandler = updatePersonaCommandHandler;
            _deletePersonaCommandHandler = deletePersonaCommandHandler;
            _getAllPersonaQueryHandler = getAllPersonaQueryHandler;
            _getByIdPersonaQueryHandler = getByIdPersonaQueryHandler;
        }

        public async Task<List<GetPersonaQueryDto>> GetAllAsync()
        {
            return await _getAllPersonaQueryHandler.Handle();
        }

        public async Task<GetPersonaQueryDto> GetByIdAsync(GetByIdPersonaQuery getByIdPersona)
        {
            return await _getByIdPersonaQueryHandler.Handle(getByIdPersona);
        }

        public async Task<PersonaCommandDto> AddAsync(CreatePersonaCommand createPersona)
        {
            return await _createPersonaCommandHandler.Handle(createPersona);
        }

        public async Task<bool> UpdateAsync(UpdatePersonaCommand updatePersona)
        {
            return await _updatePersonaCommandHandler.Handle(updatePersona);
        }

        public async Task<bool> DeleteAsync(DeletePersonaCommand deletePersona)
        {
            return await _deletePersonaCommandHandler.Handle(deletePersona);
        }
    }
}
