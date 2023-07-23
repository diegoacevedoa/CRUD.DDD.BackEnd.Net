namespace CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Update
{
    public class UpdatePersonaCommand
    {
        public UpdatePersonaCommand(int idPersona, string noDocumento, string nombres, string apellidos)
        {
            IdPersona = idPersona;
            NoDocumento = noDocumento;
            Nombres = nombres;
            Apellidos = apellidos;
        }

        public int IdPersona { get; private set; }

        public string NoDocumento { get; private set; }

        public string Nombres { get; private set; }

        public string Apellidos { get; private set; }
    }
}
