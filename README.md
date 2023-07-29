	
   	PASOS DESARROLLO CRUD CON ARQUITECTURA DDD (DRIVEN DOMAIN DESIGN).NET CORE

1- Creamos un nuevo proyecto CRUD.DDD.BackEnd.Net.API con la solución CRUD.DDD.BackEnd.Net: ASP.NET Core Web API en Visual Studio 2022, Framework 6.0.

2- Agregar el proyecto o biblioteca de clases a la solución: CRUD.DDD.BackEnd.Net.Application 

3- Agregar las carpetas Commands, Queries, DTOs y Profiles al proyecto CRUD.DDD.BackEnd.Net.Application y eliminar la clase Class1.cs

4- Agregar el proyecto o biblioteca de clases a la solución: CRUD.DDD.BackEnd.Net.Domain

5- Agregar las carpetas AggregatesModel, Helpers y SeedWork al proyecto CRUD.DDD.BackEnd.Net.Domain y eliminar la clase Class1.cs

6- Agregar el proyecto o biblioteca de clases a la solución: CRUD.DDD.BackEnd.Net.Infrastructure

7- Agregar carpeta con nombre de la base de datos "Data" y agregar las sgtes carpetas adentro de esta:
   Context, EntityConfigurations y Repositories al proyecto CRUD.DDD.BackEnd.Net.Infrastructure y eliminar la clase Class1.cs

8- Agregar Proyecto de prueba unitaria (.NET Framework) con pruebas de MSTest a la solución: CRUD.DDD.BackEnd.Net.Test

9- Agregar la carpeta Controllers al proyecto CRUD.DDD.BackEnd.Net.Test
   y eliminar la clase Class1.cs

10- Agregar dependencias de proyectos:
    CRUD.DDD.BackEnd.Net.Infrastructure Depende de CRUD.DDD.BackEnd.Net.Domain
    CRUD.DDD.BackEnd.Net.Application    Depende de CRUD.DDD.BackEnd.Net.Domain
    CRUD.DDD.BackEnd.Net.Application    Depende de CRUD.DDD.BackEnd.Net.Infrastructure
    CRUD.DDD.BackEnd.Net.API            Depende de CRUD.DDD.BackEnd.Net.Application
    CRUD.DDD.BackEnd.Net.Test           Depende de CRUD.DDD.BackEnd.Net.API

11- Agregar paquete de nuget Microsoft.EntityFrameworkCore.SqlServer en proyecto CRUD.DDD.BackEnd.Net.Infrastructure

12- Agregar paquete de nuget Dapper en proyecto CRUD.DDD.BackEnd.Net.Application

13- Agregar la interface IAggregateRoot en la carpeta SeedWork del proyecto CRUD.DDD.BackEnd.Net.Domain:

public interface IAggregateRoot { }

14- Agregar la carpeta PersonaAggregate en la carpeta AggregatesModel del proyecto CRUD.DDD.BackEnd.Net.Domain

15- Creamos atributo IdPersona de la entidad Persona en la carpeta PersonaAggregate de CRUD.DDD.BackEnd.Net.Domain como clase con sus diferentes métodos y validaciones: NoDocumento.cs

    public record IdPersona
    {
        internal IdPersona(int value)
        {
            Value = value;
        }

        public int Value { get; init; }

        public static IdPersona Create(int value)
        {
            Validate(value);

            return new IdPersona(value);
        }

        private static void Validate(int value)
        {
            if (value == 0)
            {
                throw new ArgumentException("El valor de la propiedad IdPersona no puede ser 0.");
            }
        }
    }

16- Creamos atributo NoDocumento de la entidad Persona en la carpeta PersonaAggregate de CRUD.DDD.BackEnd.Net.Domain como clase con sus diferentes métodos y validaciones: NoDocumento.cs

    public record NoDocumento
    {
        internal NoDocumento(string value)
        {
            Value = value;
        }

        public string Value { get; init; }

        public static NoDocumento Create(string value)
        {
            Validate(value);

            return new NoDocumento(value);
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("El valor de la propiedad NoDocumento no puede ser nulo o vacío.");
            }
            else if (value.Length > 50)
            {
                throw new ArgumentException("El máximo número de caracteres permitidos es 50.");
            }
        }
    }


17- Creamos atributo Nombres de la entidad Persona en la carpeta PersonaAggregate de CRUD.DDD.BackEnd.Net.Domain como clase con sus diferentes métodos y validaciones: Nombres.cs

    public record Nombres
    {
        internal Nombres(string value)
        {
            Value = value;
        }

        public string Value { get; init; }

        public static Nombres Create(string value)
        {
            Validate(value);

            return new Nombres(value);
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("El valor de la propiedad Nombres no puede ser nulo o vacío.");
            }
            else if (value.Length > 100)
            {
                throw new ArgumentException("El máximo número de caracteres permitidos es 100.");
            }
        }
    }


18- Creamos atributo Apellidos de la entidad Persona en la carpeta PersonaAggregate de CRUD.DDD.BackEnd.Net.Domain como clase con sus diferentes métodos y validaciones: Apellidos.cs

    public record Apellidos
    {
        internal Apellidos(string value)
        {
            Value = value;
        }

        public string Value { get; init; }

        public static Apellidos Create(string value)
        {
            Validate(value);

            return new Apellidos(value);
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("El valor de la propiedad Apellidos no puede ser nulo o vacío.");
            }
            else if (value.Length > 100)
            {
                throw new ArgumentException("El máximo número de caracteres permitidos es 100.");
            }
        }
    }


19- Agregar la clase Persona.cs en la carpeta PersonaAggregate de CRUD.DDD.BackEnd.Net.Domain con sus diferentes atributos y comportamientos:

    using CRUD.DDD.BackEnd.Net.Domain.SeedWork;

    public class Persona : IAggregateRoot
    {
        #region Atributos

        public int IdPersona { get; protected set; }

        public NoDocumento NoDocumento { get; private set; }

        public Nombres Nombres { get; private set; }

        public Apellidos Apellidos { get; private set; }

        #endregion

        #region Comportamientos

        public Persona() { }

        public void SetNoDocumento(NoDocumento noDocumento)
        {
            NoDocumento = noDocumento;
        }

        public void SetNombres(Nombres nombres)
        {
            Nombres = nombres;
        }

        public void SetApellidos(Apellidos apellidos)
        {
            Apellidos = apellidos;
        }

        #endregion
    }


20- Agregar la clase DataContext.cs en la carpeta Context de CRUD.DDD.BackEnd.Net.Infrastructure:

using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;
using CRUD.DDD.BackEnd.Net.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        #region DataSets

        public DbSet<Persona> Personas { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonaConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }



21- Agregar la clase PersonaConfiguration.cs en la carpeta EntityConfigurations de CRUD.DDD.BackEnd.Net.Infrastructure:

using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;
using CRUD.DDD.BackEnd.Net.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("Persona", DataContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.IdPersona);

            builder.Property(x => x.NoDocumento).IsRequired().HasMaxLength(50).HasConversion(noDocumento => noDocumento.Value, value => NoDocumento.Create(value));

            builder.Property(x => x.Nombres).IsRequired().HasMaxLength(100).HasConversion(nombres => nombres.Value, value => Nombres.Create(value));

            builder.Property(x => x.Apellidos).IsRequired().HasMaxLength(100).HasConversion(apellidos => apellidos.Value, value => Apellidos.Create(value));
        }
    }


22- Agregar la interface IPersonaRepository.cs en la carpeta PersonaAggregate de CRUD.DDD.BackEnd.Net.Domain:

    public interface IPersonaRepository
    {
        Task<List<Persona>> GetAsync();

        Task<Persona?> GetByIdAsync(IdPersona idPersona);

        Task<Persona> AddAsync(Persona persona);

        Task<bool> UpdateAsync(Persona persona);

        Task<bool> DeleteAsync(IdPersona idPersona);
    }

23- Agregar la clase PersonaRepository.cs en la carpeta Repositories de CRUD.DDD.BackEnd.Net.Infrastructure:

using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;
using CRUD.DDD.BackEnd.Net.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

    public class PersonaRepository : IPersonaRepository
    {
        private readonly DataContext _dataContext;

        public PersonaRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<List<Persona>> GetAsync()
        {
            return await _dataContext.Personas.ToListAsync();
        }

        public async Task<Persona?> GetByIdAsync(IdPersona idPersona)
        {
            return await _dataContext.Personas.FindAsync(idPersona.Value);
        }

        public async Task<Persona> AddAsync(Persona persona)
        {
            await _dataContext.Personas.AddAsync(persona);
            await _dataContext.SaveChangesAsync();

            return persona;
        }

        public async Task<bool> UpdateAsync(Persona persona)
        {
            _dataContext.Entry(persona).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(IdPersona idPersona)
        {
            var persona = await _dataContext.Personas.FindAsync(idPersona.Value);

            if (persona == null)
            {
                return await Task.FromResult(false);
            }

            _dataContext.Personas.Remove(persona);
            await _dataContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }
    }

24- Agregamos ConnectionStrings en archivo appsettings.json del proyecto inicial: CRUD.DDD.BackEnd.Net.API 

,"ConnectionStrings": {
    "DataContext": "Server=localhost\\sqlexpress;Database=Prueba;user id=diego.acevedoa;password=Medellin1*;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true"
  }

25- Instalamos paquete de nuget package manager MediatR.Extensions.Microsoft.DependencyInjection en el proyecto CRUD.DDD.BackEnd.Net.API y CRUD.DDD.BackEnd.Net.Application, 
    que sirve para mediar entre el controlador y los comandos o queries para disminuir el acoplamiento entre capas y agregamos las siguientes sentencias en el archivo Program.cs:

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(typeof(CreatePersonaCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(UpdatePersonaCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(DeletePersonaCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetByIdPersonaQuery).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetPersonaQuery).GetTypeInfo().Assembly);


26- Instalamos paquete de nuget package manager AutoMapper.Extensions.Microsoft.DependencyInjection en el proyecto CRUD.DDD.BackEnd.Net.Application, 
    que sirve para mapear las clases y agregamos las siguientes sentencias en el archivo Program.cs:

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

27- Agregar la clase MappingProfile.cs en la carpeta Profiles de CRUD.DDD.BackEnd.Net.Application donde hacemos los mapeos de las clases:

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

28- Creamos la carpeta Persona adentro de la carpeta Commands del proyecto CRUD.DDD.BackEnd.Net.Application

29- Creamos la carpeta Create adentro de la carpeta Persona de Commands del proyecto CRUD.DDD.BackEnd.Net.Application

30- Agregar la clase CreatePersonaCommand.cs en la carpeta Create de Persona de Commands de CRUD.DDD.BackEnd.Net.Application, utilizamos record en vez de class que simula una clase:

using MediatR;

    public record CreatePersonaCommand(string NoDocumento, string Nombres, string Apellidos) : IRequest<CreatePersonaCommandDto>;

31- Agregar la clase CreatePersonaCommandDto.cs en la carpeta Create de Persona de Commands de CRUD.DDD.BackEnd.Net.Application:

using System.Text.Json.Serialization;

    public class CreatePersonaCommandDto
    {
        [JsonPropertyName("idPersona")]
        public int IdPersona { get; set; }

        [JsonPropertyName("noDocumento")]
        public string NoDocumento { get; set; }

        [JsonPropertyName("nombres")]
        public string Nombres { get; set; }

        [JsonPropertyName("apellidos")]
        public string Apellidos { get; set; }
    }

32- Agregar la clase CreatePersonaCommandHandler.cs en la carpeta Create de Persona de Commands de CRUD.DDD.BackEnd.Net.Application:

using AutoMapper;
using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;
using MediatR;


    public class CreatePersonaCommandHandler : IRequestHandler<CreatePersonaCommand, CreatePersonaCommandDto>
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IMapper _mapper;

        public CreatePersonaCommandHandler(IPersonaRepository personaRepository, IMapper mapper)
        {
            _personaRepository = personaRepository;
            _mapper = mapper;
        }

        public async Task<CreatePersonaCommandDto> Handle(CreatePersonaCommand request, CancellationToken cancellationToken)
        {
            var persona = new Domain.AggregatesModel.PersonaAggregate.Persona();
            persona.SetNoDocumento(NoDocumento.Create(request.NoDocumento));
            persona.SetNombres(Nombres.Create(request.Nombres));
            persona.SetApellidos(Apellidos.Create(request.Apellidos));

            var result = await _personaRepository.AddAsync(persona);

            return _mapper.Map<CreatePersonaCommandDto>(result);

        }
    }

33- Creamos la carpeta Delete adentro de la carpeta Persona de Commands del proyecto CRUD.DDD.BackEnd.Net.Application 

34- Agregar la clase DeletePersonaCommand.cs en la carpeta Delete de Persona de Commands de CRUD.DDD.BackEnd.Net.Application, utilizamos record en vez de class que simula una clase:

using MediatR;

public record DeletePersonaCommand(int IdPersona) : IRequest<bool>;

35- Agregar la clase DeletePersonaCommandHandler.cs en la carpeta Delete de Persona de Commands de CRUD.DDD.BackEnd.Net.Application:

using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;
using MediatR;

    public class DeletePersonaCommandHandler : IRequestHandler<DeletePersonaCommand, bool>
    {
        private readonly IPersonaRepository _personaRepository;

        public DeletePersonaCommandHandler(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<bool> Handle(DeletePersonaCommand request, CancellationToken cancellationToken)
        {
            return await _personaRepository.DeleteAsync(IdPersona.Create(request.IdPersona));
        }
    }

36- Creamos la carpeta Update adentro de la carpeta Persona de Commands del proyecto CRUD.DDD.BackEnd.Net.Application 

37- Agregar la clase UpdatePersonaCommand.cs en la carpeta Update de Persona de Commands de CRUD.DDD.BackEnd.Net.Application, utilizamos record en vez de class que simula una clase:

using MediatR;

    public record UpdatePersonaCommand(int IdPersona, string NoDocumento, string Nombres, string Apellidos) : IRequest<bool>;

38- Agregar la clase UpdatePersonaCommandHandler.cs en la carpeta Update de Persona de Commands de CRUD.DDD.BackEnd.Net.Application:

using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;
using MediatR;

    public class UpdatePersonaCommandHandler : IRequestHandler<UpdatePersonaCommand, bool>
    {
        private readonly IPersonaRepository _personaRepository;

        public UpdatePersonaCommandHandler(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<bool> Handle(UpdatePersonaCommand request, CancellationToken cancellationToken)
        {
            var persona = await _personaRepository.GetByIdAsync(IdPersona.Create(request.IdPersona));

            if (persona is null)
            {
                throw new ArgumentException("No se pudo actualizar el registro.");
            }

            persona.SetNoDocumento(NoDocumento.Create(request.NoDocumento));
            persona.SetNombres(Nombres.Create(request.Nombres));
            persona.SetApellidos(Apellidos.Create(request.Apellidos));

            return await _personaRepository.UpdateAsync(persona);
        }
    }


39- Creamos la carpeta Persona adentro de la carpeta Queries del proyecto CRUD.DDD.BackEnd.Net.Application

40- Agregar la clase GetPersonaQueryDto.cs en la carpeta Persona de Queries de CRUD.DDD.BackEnd.Net.Application:

using System.Text.Json.Serialization;

    public class GetPersonaQueryDto
    {
        [JsonPropertyName("idPersona")]
        public int IdPersona { get; set; }

        [JsonPropertyName("noDocumento")]
        public string NoDocumento { get; set; }

        [JsonPropertyName("nombres")]
        public string Nombres { get; set; }

        [JsonPropertyName("apellidos")]
        public string Apellidos { get; set; }
    }

41- Agregar la clase GetPersonaQuery.cs en la carpeta Persona de Queries de CRUD.DDD.BackEnd.Net.Application:

using MediatR;

public record GetPersonaQuery : IRequest<IEnumerable<GetPersonaQueryDto>>;


42- Agregar la clase GetAllPersonaQueryHandler.cs en la carpeta Persona de Queries de CRUD.DDD.BackEnd.Net.Application:

using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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

43- Agregar la clase GetByIdPersonaQuery.cs en la carpeta Persona de Queries de CRUD.DDD.BackEnd.Net.Application:

using MediatR;

    public record GetByIdPersonaQuery(int IdPersona) : IRequest<GetPersonaQueryDto?>;


44- Agregar la clase GetByIdPersonaQueryHandler.cs en la carpeta Persona de Queries de CRUD.DDD.BackEnd.Net.Application:

using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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


45- Agregar la clase CreatePersonaDto.cs en la carpeta DTOs de CRUD.DDD.BackEnd.Net.Application:

using System.ComponentModel.DataAnnotations;

    public class CreatePersonaDto
    {
        [Required(ErrorMessage = "El campo NoDocumento es requerido.")]
        [StringLength(50)]
        public string NoDocumento { get; set; }

        [Required(ErrorMessage = "El campo Nombres es requerido.")]
        [StringLength(100)]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo Apellidos es requerido.")]
        [StringLength(100)]
        public string Apellidos { get; set; }
    }


46- Agregar la clase UpdatePersonaDto.cs en la carpeta DTOs de CRUD.DDD.BackEnd.Net.Application:

    public class UpdatePersonaDto
    {
        [Required(ErrorMessage = "El campo IdPersona es requerido.")]
        public int IdPersona { get; set; }

        [Required(ErrorMessage = "El campo NoDocumento es requerido.")]
        [StringLength(50)]
        public string NoDocumento { get; set; }

        [Required(ErrorMessage = "El campo Nombres es requerido.")]
        [StringLength(100)]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo Apellidos es requerido.")]
        [StringLength(100)]
        public string Apellidos { get; set; }
    }


47- Agregar "Controlador de API con acciones de lectura y escritura": PersonasController.cs

using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Create;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Delete;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Update;
using CRUD.DDD.BackEnd.Net.Application.DTOs;
using CRUD.DDD.BackEnd.Net.Application.Queries.Persona;
using MediatR;
using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly ISender _sender;

        public PersonasController(ISender sender)
        {
            _sender = sender;
        }

        // GET: api/<PersonasController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPersonaQueryDto>>> GetAllPersona()
        {
            var personas = await _sender.Send(new GetPersonaQuery());

            return Ok(personas);
        }

        // GET api/<PersonasController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPersonaQueryDto?>> GetByIdPersona(int id)
        {
            return await _sender.Send(new GetByIdPersonaQuery(id));
        }

        // POST api/<PersonasController>
        [HttpPost]
        public async Task<ActionResult<CreatePersonaCommandDto>> Post([FromBody] CreatePersonaDto createPersona)
        {
            var result = await _sender.Send(new CreatePersonaCommand(createPersona.NoDocumento, createPersona.Nombres, createPersona.Apellidos));

            return CreatedAtAction("GetByIdPersona", new { id = result.IdPersona }, result);
        }

        // PUT api/<PersonasController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdatePersonaDto updatePersona)
        {
            if (id != updatePersona.IdPersona)
            {
                return BadRequest();
            }

            await _sender.Send(new UpdatePersonaCommand(id, updatePersona.NoDocumento, updatePersona.Nombres, updatePersona.Apellidos));

            return NoContent();
        }

        // DELETE api/<PersonasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sender.Send(new DeletePersonaCommand(id));

            return NoContent();
        }
    }


48- Agregar Cors en archivo Program.cs después de builder:

builder.Services.AddCors(options =>
{
    options.AddPolicy("Personas.CORS", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


app.UseCors("Personas.CORS");

49- Agregar ConnectionStrings en archivo Program.cs después de builder:

using CRUD.DDD.BackEnd.Net.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext") ?? throw new InvalidOperationException("Connection string 'DataContext' not found.")));


50- Realizamos inyección de dependencias en archivo Program.cs después de builder:

builder.Services.AddTransient<IPersonaRepository, PersonaRepository>();
builder.Services.AddTransient<CreatePersonaCommandHandler>();
builder.Services.AddTransient<DeletePersonaCommandHandler>();
builder.Services.AddTransient<UpdatePersonaCommandHandler>();
builder.Services.AddTransient<GetAllPersonaQueryHandler>();
builder.Services.AddTransient<GetByIdPersonaQueryHandler>();

51- El archivo Program.cs del proyecto CRUD.DDD.BackEnd.Net.API queda así:

using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Create;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Delete;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Update;
using CRUD.DDD.BackEnd.Net.Application.Queries.Persona;
using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;
using CRUD.DDD.BackEnd.Net.Infrastructure.Data.Context;
using CRUD.DDD.BackEnd.Net.Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper;
using CRUD.DDD.BackEnd.Net.Application.Profiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext") ?? throw new InvalidOperationException("Connection string 'DataContext' not found.")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Personas.CORS", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Add services to the container.

builder.Services.AddTransient<IPersonaRepository, PersonaRepository>();
builder.Services.AddTransient<CreatePersonaCommandHandler>();
builder.Services.AddTransient<DeletePersonaCommandHandler>();
builder.Services.AddTransient<UpdatePersonaCommandHandler>();
builder.Services.AddTransient<GetAllPersonaQueryHandler>();
builder.Services.AddTransient<GetByIdPersonaQueryHandler>();

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(typeof(CreatePersonaCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(UpdatePersonaCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(DeletePersonaCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetByIdPersonaQuery).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetPersonaQuery).GetTypeInfo().Assembly);

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Personas.CORS");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

52- Ejecutar y probar