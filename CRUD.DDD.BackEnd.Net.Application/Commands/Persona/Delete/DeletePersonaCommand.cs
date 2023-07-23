namespace CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Delete
{
    public class DeletePersonaCommand
    {
        public DeletePersonaCommand(int idPersona)
        {
            IdPersona = idPersona;
        }

        public int IdPersona { get; private set; }
    }
}
