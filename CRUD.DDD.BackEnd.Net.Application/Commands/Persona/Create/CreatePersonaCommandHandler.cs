using AutoMapper;
using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;
using MediatR;

namespace CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Create
{
    public class CreatePersonaCommandHandler : IRequestHandler<CreatePersonaCommand, CreatePersonaCommandDto>
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IMapper _mapper;

        public CreatePersonaCommandHandler(IPersonaRepository personaRepository, IMapper mapper)
        {
            _personaRepository = personaRepository;
            _mapper = mapper;
        }

        public async Task<CreatePersonaCommandDto> Handle(CreatePersonaCommand request, CancellationToken cancellationToken)
        {
            var persona = new Domain.AggregatesModel.PersonaAggregate.Persona();
            persona.SetNoDocumento(NoDocumento.Create(request.NoDocumento));
            persona.SetNombres(Nombres.Create(request.Nombres));
            persona.SetApellidos(Apellidos.Create(request.Apellidos));

            var result = await _personaRepository.AddAsync(persona);

            return _mapper.Map<CreatePersonaCommandDto>(result);

        }
    }
}
