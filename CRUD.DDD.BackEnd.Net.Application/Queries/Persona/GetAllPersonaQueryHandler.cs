using Dapper;
using Microsoft.Data.SqlClient;

namespace CRUD.DDD.BackEnd.Net.Application.Queries.Persona
{
    public class GetAllPersonaQueryHandler
    {
        private readonly string _connectionString;

        public GetAllPersonaQueryHandler(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<GetPersonaQueryDto>> Handle()
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
