using MediatR;

namespace CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Delete
{
    public record DeletePersonaCommand(int IdPersona) : IRequest<bool>;
}
