using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;
using CRUD.DDD.BackEnd.Net.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUD.DDD.BackEnd.Net.Infrastructure.Data.EntityConfigurations
{
    public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("Persona", DataContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.IdPersona);

            builder.Property(x => x.NoDocumento).IsRequired().HasMaxLength(50).HasConversion(noDocumento => noDocumento.Value, value => NoDocumento.Create(value));

            builder.Property(x => x.Nombres).IsRequired().HasMaxLength(100).HasConversion(nombres => nombres.Value, value => Nombres.Create(value));

            builder.Property(x => x.Apellidos).IsRequired().HasMaxLength(100).HasConversion(apellidos => apellidos.Value, value => Apellidos.Create(value));
        }
    }
}
