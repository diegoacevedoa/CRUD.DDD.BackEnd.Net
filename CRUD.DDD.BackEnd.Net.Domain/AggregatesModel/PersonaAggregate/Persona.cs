using CRUD.DDD.BackEnd.Net.Domain.SeedWork;

namespace CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate
{
    public class Persona : IAggregateRoot
    {
        #region Atributos

        public int IdPersona { get; protected set; }

        public NoDocumento NoDocumento { get; private set; }

        public Nombres Nombres { get; private set; }

        public Apellidos Apellidos { get; private set; }

        #endregion

        #region Comportamientos

        public Persona() { }

        public void SetNoDocumento(NoDocumento noDocumento)
        {
            NoDocumento = noDocumento;
        }

        public void SetNombres(Nombres nombres)
        {
            Nombres = nombres;
        }

        public void SetApellidos(Apellidos apellidos)
        {
            Apellidos = apellidos;
        }

        #endregion
    }
}
