using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;

namespace CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Create
{
    public class CreatePersonaCommandHandler
    {
        private readonly IPersonaRepository _personaRepository;

        public CreatePersonaCommandHandler(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<PersonaCommandDto> Handle(CreatePersonaCommand createPersona)
        {
            var persona = new Domain.AggregatesModel.PersonaAggregate.Persona();
            persona.SetNoDocumento(NoDocumento.Create(createPersona.NoDocumento));
            persona.SetNombres(Nombres.Create(createPersona.Nombres));
            persona.SetApellidos(Apellidos.Create(createPersona.Apellidos));

            var result = await _personaRepository.AddAsync(persona);

            return new PersonaCommandDto() { IdPersona = result.IdPersona, NoDocumento = result.NoDocumento.Value, Nombres = result.Nombres.Value, Apellidos = result.Apellidos.Value };
        }
    }
}
