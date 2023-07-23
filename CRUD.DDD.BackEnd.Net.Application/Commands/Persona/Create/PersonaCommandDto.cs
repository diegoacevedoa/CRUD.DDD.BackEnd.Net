namespace CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Create
{
    public class PersonaCommandDto
    {
        public int IdPersona { get; set; }

        public string NoDocumento { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }
    }
}
