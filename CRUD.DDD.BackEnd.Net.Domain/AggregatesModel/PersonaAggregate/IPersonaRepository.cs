namespace CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate
{
    public interface IPersonaRepository
    {
        Task<List<Persona>> GetAsync();

        Task<Persona?> GetByIdAsync(IdPersona idPersona);

        Task<Persona> AddAsync(Persona persona);

        Task<bool> UpdateAsync(Persona persona);

        Task<bool> DeleteAsync(IdPersona idPersona);
    }
}
