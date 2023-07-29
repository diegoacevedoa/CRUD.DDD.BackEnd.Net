using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;
using MediatR;

namespace CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Delete
{
    public class DeletePersonaCommandHandler : IRequestHandler<DeletePersonaCommand, bool>
    {
        private readonly IPersonaRepository _personaRepository;

        public DeletePersonaCommandHandler(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<bool> Handle(DeletePersonaCommand request, CancellationToken cancellationToken)
        {
            return await _personaRepository.DeleteAsync(IdPersona.Create(request.IdPersona));
        }
    }
}
