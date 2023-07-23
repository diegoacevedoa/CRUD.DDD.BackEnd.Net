using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;

namespace CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Delete
{
    public class DeletePersonaCommandHandler
    {
        private readonly IPersonaRepository _personaRepository;

        public DeletePersonaCommandHandler(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<bool> Handle(DeletePersonaCommand deletePersona)
        {
            return await _personaRepository.DeleteAsync(IdPersona.Create(deletePersona.IdPersona));
        }
    }
}
