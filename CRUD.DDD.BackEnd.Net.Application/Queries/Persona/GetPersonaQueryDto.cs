namespace CRUD.DDD.BackEnd.Net.Application.Queries.Persona
{
    public class GetPersonaQueryDto
    {
        public int IdPersona { get; set; }

        public string NoDocumento { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }
    }
}
