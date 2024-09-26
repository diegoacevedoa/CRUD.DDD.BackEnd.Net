using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Create;
using CRUD.DDD.BackEnd.Net.Application.Queries.Persona;
using FluentAssertions;
using System.Net.Http.Formatting;

namespace CRUD.DDD.BackEnd.Net.Test.API
{
    public class PersonaControllerTest : IntegrationTestBuilder
    {

        [Fact]
        public void GetPersonaExito()
        {
            var result = this.TestClient.GetAsync("api/Personas").Result;
            result.EnsureSuccessStatusCode();

            var response = result.Content.ReadAsStringAsync().Result;
            var responseQuery = System.Text.Json.JsonSerializer.Deserialize<List<GetPersonaQueryDto>>(response);

            responseQuery.Should().NotBeNull();
        }

        [Fact]
        public void PostCreatePersonaExito()
        {
            var noDocumento1 = "12345";
            var nombres1 = "diego";
            var apellidos1 = "acevedo";

            var result = this.TestClient.PostAsync("api/Personas", new CreatePersonaCommand(noDocumento1, nombres1, apellidos1), new JsonMediaTypeFormatter()).Result;
            result.EnsureSuccessStatusCode();

            var response = result.Content.ReadAsStringAsync().Result;
            var responseQuery = System.Text.Json.JsonSerializer.Deserialize<GetPersonaQueryDto>(response);

            responseQuery.Should().NotBeNull();
            responseQuery.NoDocumento.Should().Be(noDocumento1);
            responseQuery.Nombres.Should().Be(nombres1);
            responseQuery.Apellidos.Should().Be(apellidos1);
        }
    }
}
