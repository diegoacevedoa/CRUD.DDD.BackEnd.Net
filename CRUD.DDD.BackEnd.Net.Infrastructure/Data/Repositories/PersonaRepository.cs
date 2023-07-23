using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;
using CRUD.DDD.BackEnd.Net.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CRUD.DDD.BackEnd.Net.Infrastructure.Data.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly DataContext _dataContext;

        public PersonaRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<List<Persona>> GetAsync()
        {
            return await _dataContext.Personas.ToListAsync();
        }

        public async Task<Persona?> GetByIdAsync(IdPersona idPersona)
        {
            return await _dataContext.Personas.FindAsync(idPersona.Value);
        }

        public async Task<Persona> AddAsync(Persona persona)
        {
            await _dataContext.Personas.AddAsync(persona);
            await _dataContext.SaveChangesAsync();

            return persona;
        }

        public async Task<bool> UpdateAsync(Persona persona)
        {
            _dataContext.Entry(persona).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(IdPersona idPersona)
        {
            var persona = await _dataContext.Personas.FindAsync(idPersona.Value);

            if (persona == null)
            {
                return await Task.FromResult(false);
            }

            _dataContext.Personas.Remove(persona);
            await _dataContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }
    }
}
