using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;
using CRUD.DDD.BackEnd.Net.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CRUD.DDD.BackEnd.Net.Infrastructure.Data.Context
{
    public class DataContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        #region DataSets

        public DbSet<Persona> Personas { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonaConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
