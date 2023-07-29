using MediatR;

namespace CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Create
{
    public record CreatePersonaCommand(string NoDocumento, string Nombres, string Apellidos) : IRequest<CreatePersonaCommandDto>;
}
