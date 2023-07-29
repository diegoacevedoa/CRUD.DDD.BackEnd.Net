using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CRUD.DDD.BackEnd.Net.Application.Queries.Persona
{
    public class GetAllPersonaQueryHandler : IRequestHandler<GetPersonaQuery, IEnumerable<GetPersonaQueryDto>>
    {
        private readonly string _connectionString;

        public GetAllPersonaQueryHandler(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DataContext") ?? throw new InvalidOperationException("Connection string 'DataContext' not found.");
        }

        public async Task<IEnumerable<GetPersonaQueryDto>> Handle(GetPersonaQuery request, CancellationToken cancellationToken)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string queryString = string.Format("{0}", @"SELECT [IdPersona], [NoDocumento], [Nombres], [Apellidos]
                                                        FROM [dbo].[Persona] ");

            var list = await connection.QueryAsync<GetPersonaQueryDto>(queryString);

            return list.AsList();
        }
    }
}
