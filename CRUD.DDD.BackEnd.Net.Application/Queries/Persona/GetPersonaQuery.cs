using MediatR;

namespace CRUD.DDD.BackEnd.Net.Application.Queries.Persona
{
    public record GetPersonaQuery : IRequest<IEnumerable<GetPersonaQueryDto>>;
}
