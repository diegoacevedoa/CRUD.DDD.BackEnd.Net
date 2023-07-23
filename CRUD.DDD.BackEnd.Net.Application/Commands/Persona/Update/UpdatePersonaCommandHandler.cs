using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;

namespace CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Update
{
    public class UpdatePersonaCommandHandler
    {
        private readonly IPersonaRepository _personaRepository;

        public UpdatePersonaCommandHandler(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<bool> Handle(UpdatePersonaCommand updatePersona)
        {
            var persona = await _personaRepository.GetByIdAsync(IdPersona.Create(updatePersona.IdPersona));

            if (persona is null)
            {
                throw new ArgumentException("No se pudo actualizar el registro.");
            }

            persona.SetNoDocumento(NoDocumento.Create(updatePersona.NoDocumento));
            persona.SetNombres(Nombres.Create(updatePersona.Nombres));
            persona.SetApellidos(Apellidos.Create(updatePersona.Apellidos));

            return await _personaRepository.UpdateAsync(persona);
        }
    }
}
