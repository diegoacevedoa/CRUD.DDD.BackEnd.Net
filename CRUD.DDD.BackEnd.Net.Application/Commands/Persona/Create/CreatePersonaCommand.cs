namespace CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Create
{
    public class CreatePersonaCommand
    {
        public CreatePersonaCommand(string noDocumento, string nombres, string apellidos)
        {
            NoDocumento = noDocumento;
            Nombres = nombres;
            Apellidos = apellidos;
        }

        public string NoDocumento { get; private set; }

        public string Nombres { get; private set; }

        public string Apellidos { get; private set; }
    }
}
