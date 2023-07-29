using MediatR;

namespace CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Update
{
    public record UpdatePersonaCommand(int IdPersona, string NoDocumento, string Nombres, string Apellidos) : IRequest<bool>;
}
