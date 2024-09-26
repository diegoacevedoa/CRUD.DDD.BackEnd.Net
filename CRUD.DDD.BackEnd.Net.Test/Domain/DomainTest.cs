using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;
using FluentAssertions;

namespace CRUD.DDD.BackEnd.Net.Test.Domain
{
    public class DomainTest
    {
        /// <summary>
        /// Se prueba que se cree bien la persona
        /// </summary>
        [Fact]
        public void CreatePersonaTest1()
        {
            var idPersona1 = 0;
            var noDocumento1 = "12345";
            var nombres1 = "diego";
            var apellidos1 = "acevedo";

            var persona1 = new Persona();

            persona1.SetNoDocumento(NoDocumento.Create(noDocumento1));
            persona1.SetNombres(Nombres.Create(nombres1));
            persona1.SetApellidos(Apellidos.Create(apellidos1));

            persona1.Should().NotBeNull();
            persona1.NoDocumento.Value.Should().Be(noDocumento1);
            persona1.Nombres.Value.Should().Be(nombres1);
            persona1.Apellidos.Value.Should().Be(apellidos1);
        }

    }
}
