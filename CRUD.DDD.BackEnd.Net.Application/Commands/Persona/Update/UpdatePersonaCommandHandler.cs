using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;
using MediatR;

namespace CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Update
{
    public class UpdatePersonaCommandHandler : IRequestHandler<UpdatePersonaCommand, bool>
    {
        private readonly IPersonaRepository _personaRepository;

        public UpdatePersonaCommandHandler(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<bool> Handle(UpdatePersonaCommand request, CancellationToken cancellationToken)
        {
            var persona = await _personaRepository.GetByIdAsync(IdPersona.Create(request.IdPersona));

            if (persona is null)
            {
                throw new ArgumentException("No se pudo actualizar el registro.");
            }

            persona.SetNoDocumento(NoDocumento.Create(request.NoDocumento));
            persona.SetNombres(Nombres.Create(request.Nombres));
            persona.SetApellidos(Apellidos.Create(request.Apellidos));

            return await _personaRepository.UpdateAsync(persona);
        }
    }
}
