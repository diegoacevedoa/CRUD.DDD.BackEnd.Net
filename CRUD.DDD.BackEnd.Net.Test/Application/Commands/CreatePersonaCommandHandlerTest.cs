using AutoMapper;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Create;
using CRUD.DDD.BackEnd.Net.Application.Profiles;
using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;
using CRUD.DDD.BackEnd.Net.Test.Mocks;
using FluentAssertions;
using Moq;

namespace CRUD.DDD.BackEnd.Net.Test.Application.Commands
{
    public class CreatePersonaCommandHandlerTest
    {
        private readonly Mock<IPersonaRepository> _mockRepo;
        private readonly IMapper _mapper;

        public CreatePersonaCommandHandlerTest()
        {
            _mockRepo = MockPersonaRepository.GetPersonaRepository();
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
            _mapper = new Mapper(mappingConfig);
        }

        /// <summary>
        /// Se prueba que se cree bien la Persona
        /// </summary>
        [Fact]
        public async Task CreatePersonaCommandTest1()
        {
            var idPersona1 = 1;
            var noDocumento1 = "12345";
            var nombres1 = "diego";
            var apellidos1 = "acevedo";

            var handler = new CreatePersonaCommandHandler(_mockRepo.Object, _mapper);

            var persona1 = await handler.Handle(new CreatePersonaCommand(noDocumento1, nombres1, apellidos1), CancellationToken.None);

            persona1.Should().NotBeNull();
            persona1.NoDocumento.Should().Be(noDocumento1);
            persona1.Nombres.Should().Be(nombres1);
            persona1.Apellidos.Should().Be(apellidos1);
        }
    }
}
