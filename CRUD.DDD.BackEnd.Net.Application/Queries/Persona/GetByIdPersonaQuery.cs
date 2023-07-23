namespace CRUD.DDD.BackEnd.Net.Application.Queries.Persona
{

    public class GetByIdPersonaQuery
    {
        public GetByIdPersonaQuery(int idPersona)
        {
            IdPersona = idPersona;
        }

        public int IdPersona { get; private set; }
    }
}
