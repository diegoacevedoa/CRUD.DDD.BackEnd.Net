using AutoMapper;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Create;
using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;

namespace CRUD.DDD.BackEnd.Net.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Persona, CreatePersonaCommandDto>()
                .ForMember(m => m.IdPersona, map => map.MapFrom(vm => vm.IdPersona))
                .ForMember(m => m.NoDocumento, map => map.MapFrom(vm => vm.NoDocumento.Value))
                .ForMember(m => m.Nombres, map => map.MapFrom(vm => vm.Nombres.Value))
                .ForMember(m => m.Apellidos, map => map.MapFrom(vm => vm.Apellidos.Value))
                .ReverseMap();
        }
    }
}
