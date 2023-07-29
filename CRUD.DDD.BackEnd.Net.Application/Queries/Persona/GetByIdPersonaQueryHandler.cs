using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CRUD.DDD.BackEnd.Net.Application.Queries.Persona
{
    public class GetByIdPersonaQueryHandler : IRequestHandler<GetByIdPersonaQuery, GetPersonaQueryDto?>
    {
        private readonly string _connectionString;

        public GetByIdPersonaQueryHandler(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DataContext") ?? throw new InvalidOperationException("Connection string 'DataContext' not found.");
        }

        public async Task<GetPersonaQueryDto?> Handle(GetByIdPersonaQuery request, CancellationToken cancellationToken)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string queryString = string.Format("{0}{1}", @"SELECT [IdPersona], [NoDocumento], [Nombres], [Apellidos]
                                                        FROM [dbo].[Persona] 
                                                        WHERE [IdPersona] = ", request.IdPersona);

            var persona = await connection.QueryAsync<GetPersonaQueryDto>(queryString);

            return persona.FirstOrDefault();
        }
    }
}
