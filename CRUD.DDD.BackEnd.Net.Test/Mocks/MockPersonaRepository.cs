using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;
using Moq;

namespace CRUD.DDD.BackEnd.Net.Test.Mocks
{
    public class MockPersonaRepository
    {
        public static Mock<IPersonaRepository> GetPersonaRepository()
        {
            var idPersona1 = 1;
            var noDocumento1 = "12345";
            var nombres1 = "diego";
            var apellidos1 = "acevedo";

            var idPersona2 = 2;
            var noDocumento2 = "12346";
            var nombres2 = "mary";
            var apellidos2 = "uribe";

            var persona1 = new Persona();

            persona1.SetNoDocumento(NoDocumento.Create(noDocumento1));
            persona1.SetNombres(Nombres.Create(nombres1));
            persona1.SetApellidos(Apellidos.Create(apellidos1));

            var persona2 = new Persona();

            persona2.SetNoDocumento(NoDocumento.Create(noDocumento2));
            persona2.SetNombres(Nombres.Create(nombres2));
            persona2.SetApellidos(Apellidos.Create(apellidos2));

            var listPersona = new List<Persona>
            {
                persona1,
                persona2
            };

            var mockRepo = new Mock<IPersonaRepository>();

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Persona>())).ReturnsAsync(persona1);
            //mockRepo.Setup(r => r.AddAsync(It.IsAny<Persona>())).ReturnsAsync(persona2);
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<IdPersona>())).ReturnsAsync(persona1);
            mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(listPersona);
            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Persona>())).ReturnsAsync(true);
            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<IdPersona>())).ReturnsAsync(true);

            return mockRepo;

        }
    }

}
