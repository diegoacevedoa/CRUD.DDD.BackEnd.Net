using MediatR;

namespace CRUD.DDD.BackEnd.Net.Application.Queries.Persona
{

    public record GetByIdPersonaQuery(int IdPersona) : IRequest<GetPersonaQueryDto?>;
}
