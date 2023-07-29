using System.Text.Json.Serialization;

namespace CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Create
{
    public class CreatePersonaCommandDto
    {
        [JsonPropertyName("idPersona")]
        public int IdPersona { get; set; }

        [JsonPropertyName("noDocumento")]
        public string NoDocumento { get; set; }

        [JsonPropertyName("nombres")]
        public string Nombres { get; set; }

        [JsonPropertyName("apellidos")]
        public string Apellidos { get; set; }
    }
}
