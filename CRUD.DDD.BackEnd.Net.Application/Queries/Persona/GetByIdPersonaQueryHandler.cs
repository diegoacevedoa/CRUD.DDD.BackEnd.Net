using Dapper;
using Microsoft.Data.SqlClient;

namespace CRUD.DDD.BackEnd.Net.Application.Queries.Persona
{
    public class GetByIdPersonaQueryHandler
    {
        private readonly string _connectionString;

        public GetByIdPersonaQueryHandler(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<GetPersonaQueryDto> Handle(GetByIdPersonaQuery getByIdPersona)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string queryString = string.Format("{0}{1}", @"SELECT [IdPersona], [NoDocumento], [Nombres], [Apellidos]
                                                        FROM [dbo].[Persona] 
                                                        WHERE [IdPersona] = ", getByIdPersona.IdPersona);

            var persona = await connection.QueryAsync<GetPersonaQueryDto>(queryString);

            return persona.FirstOrDefault();
        }
    }
}
