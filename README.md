	
   	PASOS DESARROLLO CRUD CON ARQUITECTURA DDD (DRIVEN DOMAIN DESIGN).NET CORE

1- Creamos un nuevo proyecto CRUD.DDD.BackEnd.Net.API con la solución CRUD.DDD.BackEnd.Net: ASP.NET Core Web API en Visual Studio 2022, Framework 6.0.

2- Agregar el proyecto o biblioteca de clases a la solución: CRUD.DDD.BackEnd.Net.Application 

3- Agregar las carpetas Commands, Queries y Services al proyecto CRUD.DDD.BackEnd.Net.Application y eliminar la clase Class1.cs

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

13- Agregar la interface IAggregateRoot en la carpeta SeedWork del proyecto CRUD.DDD.BackEnd.Net.Domain

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

25- Creamos la carpeta Persona adentro de la carpeta Commands del proyecto CRUD.DDD.BackEnd.Net.Application

26- Creamos la carpeta Create adentro de la carpeta Persona de Commands del proyecto CRUD.DDD.BackEnd.Net.Application

27- Agregar la clase CreatePersonaCommand.cs en la carpeta Create de Persona de Commands de CRUD.DDD.BackEnd.Net.Application:

    public class CreatePersonaCommand
    {
        public CreatePersonaCommand(string noDocumento, string nombres, string apellidos)
        {
            NoDocumento = noDocumento;
            Nombres = nombres;
            Apellidos = apellidos;
        }

        public string NoDocumento { get; private set; }

        public string Nombres { get; private set; }

        public string Apellidos { get; private set; }
    }

28- Agregar la clase PersonaCommandDto.cs en la carpeta Create de Persona de Commands de CRUD.DDD.BackEnd.Net.Application:

    public class PersonaCommandDto
    {
        public int IdPersona { get; set; }

        public string NoDocumento { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }
    }

28- Agregar la clase CreatePersonaCommandHandler.cs en la carpeta Create de Persona de Commands de CRUD.DDD.BackEnd.Net.Application:

using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;

    public class CreatePersonaCommandHandler
    {
        private readonly IPersonaRepository _personaRepository;

        public CreatePersonaCommandHandler(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<PersonaCommandDto> Handle(CreatePersonaCommand createPersona)
        {
            var persona = new Domain.AggregatesModel.PersonaAggregate.Persona();
            persona.SetNoDocumento(NoDocumento.Create(createPersona.NoDocumento));
            persona.SetNombres(Nombres.Create(createPersona.Nombres));
            persona.SetApellidos(Apellidos.Create(createPersona.Apellidos));

            var result = await _personaRepository.AddAsync(persona);

            return new PersonaCommandDto() { IdPersona = result.IdPersona, NoDocumento = result.NoDocumento.Value, Nombres = result.Nombres.Value, Apellidos = result.Apellidos.Value };
        }
    }

29- Creamos la carpeta Delete adentro de la carpeta Persona de Commands del proyecto CRUD.DDD.BackEnd.Net.Application 

30- Agregar la clase DeletePersonaCommand.cs en la carpeta Delete de Persona de Commands de CRUD.DDD.BackEnd.Net.Application:

    public class DeletePersonaCommand
    {
        public DeletePersonaCommand(int idPersona)
        {
            IdPersona = idPersona;
        }

        public int IdPersona { get; private set; }
    }

31- Agregar la clase DeletePersonaCommandHandler.cs en la carpeta Delete de Persona de Commands de CRUD.DDD.BackEnd.Net.Application:

using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;

    public class DeletePersonaCommandHandler
    {
        private readonly IPersonaRepository _personaRepository;

        public DeletePersonaCommandHandler(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<bool> Handle(DeletePersonaCommand deletePersona)
        {
            return await _personaRepository.DeleteAsync(IdPersona.Create(deletePersona.IdPersona));
        }
    }


32- Creamos la carpeta Update adentro de la carpeta Persona de Commands del proyecto CRUD.DDD.BackEnd.Net.Application 

33- Agregar la clase UpdatePersonaCommand.cs en la carpeta Update de Persona de Commands de CRUD.DDD.BackEnd.Net.Application:

    public class UpdatePersonaCommand
    {
        public UpdatePersonaCommand(int idPersona, string noDocumento, string nombres, string apellidos)
        {
            IdPersona = idPersona;
            NoDocumento = noDocumento;
            Nombres = nombres;
            Apellidos = apellidos;
        }

        public int IdPersona { get; private set; }

        public string NoDocumento { get; private set; }

        public string Nombres { get; private set; }

        public string Apellidos { get; private set; }
    }

34- Agregar la clase UpdatePersonaCommandHandler.cs en la carpeta Update de Persona de Commands de CRUD.DDD.BackEnd.Net.Application:

using CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate;

    public class UpdatePersonaCommandHandler
    {
        private readonly IPersonaRepository _personaRepository;

        public UpdatePersonaCommandHandler(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<bool> Handle(UpdatePersonaCommand updatePersona)
        {
            var persona = await _personaRepository.GetByIdAsync(IdPersona.Create(updatePersona.IdPersona));

            if (persona is null)
            {
                throw new ArgumentException("No se pudo actualizar el registro.");
            }

            persona.SetNoDocumento(NoDocumento.Create(updatePersona.NoDocumento));
            persona.SetNombres(Nombres.Create(updatePersona.Nombres));
            persona.SetApellidos(Apellidos.Create(updatePersona.Apellidos));

            return await _personaRepository.UpdateAsync(persona);
        }
    }


35- Creamos la carpeta Persona adentro de la carpeta Queries del proyecto CRUD.DDD.BackEnd.Net.Application

36- Agregar la clase GetPersonaQueryDto.cs en la carpeta Persona de Queries de CRUD.DDD.BackEnd.Net.Application:

    public class GetPersonaQueryDto
    {
        public int IdPersona { get; set; }

        public string NoDocumento { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }
    }


37- Agregar la clase GetAllPersonaQueryHandler.cs en la carpeta Persona de Queries de CRUD.DDD.BackEnd.Net.Application:

    using Dapper;
    using Microsoft.Data.SqlClient;

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

38- Agregar la clase GetByIdPersonaQuery.cs en la carpeta Persona de Queries de CRUD.DDD.BackEnd.Net.Application:

    public class GetByIdPersonaQuery
    {
        public GetByIdPersonaQuery(int idPersona)
        {
            IdPersona = idPersona;
        }

        public int IdPersona { get; private set; }
    }


39- Agregar la clase GetByIdPersonaQueryHandler.cs en la carpeta Persona de Queries de CRUD.DDD.BackEnd.Net.Application:

using Dapper;
using Microsoft.Data.SqlClient;

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


40- Creamos la carpeta Persona adentro de la carpeta Services del proyecto CRUD.DDD.BackEnd.Net.Application

41- Agregar la clase IPersonaService.cs en la carpeta Persona de Services de CRUD.DDD.BackEnd.Net.Application:

using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Create;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Delete;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Update;
using CRUD.DDD.BackEnd.Net.Application.Queries.Persona;

     public interface IPersonaService
    {
        Task<List<GetPersonaQueryDto>> GetAllAsync();

        Task<GetPersonaQueryDto> GetByIdAsync(GetByIdPersonaQuery getByIdPersona);

        Task<PersonaCommandDto> AddAsync(CreatePersonaCommand createPersona);

        Task<bool> UpdateAsync(UpdatePersonaCommand updatePersona);

        Task<bool> DeleteAsync(DeletePersonaCommand deletePersona);
    }


42- Agregar la clase PersonaService.cs en la carpeta Persona de Services de CRUD.DDD.BackEnd.Net.Application:

using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Create;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Delete;
using CRUD.DDD.BackEnd.Net.Application.Commands.Persona.Update;
using CRUD.DDD.BackEnd.Net.Application.Queries.Persona;

    public class PersonaService : IPersonaService
    {
        private readonly CreatePersonaCommandHandler _createPersonaCommandHandler;
        private readonly UpdatePersonaCommandHandler _updatePersonaCommandHandler;
        private readonly DeletePersonaCommandHandler _deletePersonaCommandHandler;
        private readonly GetAllPersonaQueryHandler _getAllPersonaQueryHandler;
        private readonly GetByIdPersonaQueryHandler _getByIdPersonaQueryHandler;

        public PersonaService(
            CreatePersonaCommandHandler createPersonaCommandHandler,
            UpdatePersonaCommandHandler updatePersonaCommandHandler,
            DeletePersonaCommandHandler deletePersonaCommandHandler,
            GetAllPersonaQueryHandler getAllPersonaQueryHandler,
             GetByIdPersonaQueryHandler getByIdPersonaQueryHandler
            )
        {
            _createPersonaCommandHandler = createPersonaCommandHandler;
            _updatePersonaCommandHandler = updatePersonaCommandHandler;
            _deletePersonaCommandHandler = deletePersonaCommandHandler;
            _getAllPersonaQueryHandler = getAllPersonaQueryHandler;
            _getByIdPersonaQueryHandler = getByIdPersonaQueryHandler;
        }

        public async Task<List<GetPersonaQueryDto>> GetAllAsync()
        {
            return await _getAllPersonaQueryHandler.Handle();
        }

        public async Task<GetPersonaQueryDto> GetByIdAsync(GetByIdPersonaQuery getByIdPersona)
        {
            return await _getByIdPersonaQueryHandler.Handle(getByIdPersona);
        }

        public async Task<PersonaCommandDto> AddAsync(CreatePersonaCommand createPersona)
        {
            return await _createPersonaCommandHandler.Handle(createPersona);
        }

        public async Task<bool> UpdateAsync(UpdatePersonaCommand updatePersona)
        {
            return await _updatePersonaCommandHandler.Handle(updatePersona);
        }

        public async Task<bool> DeleteAsync(DeletePersonaCommand deletePersona)
        {
            return await _deletePersonaCommandHandler.Handle(deletePersona);
        }
    }


43- Agregar "Controlador de API con acciones de lectura y escritura": PersonasController.cs


 [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaService _personaService;

        public PersonasController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        // GET: api/<PersonasController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPersonaQueryDto>>> GetAllPersona()
        {
            return await _personaService.GetAllAsync();
        }

        // GET api/<PersonasController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPersonaQueryDto>> GetByIdPersona(int id)
        {
            return await _personaService.GetByIdAsync(new GetByIdPersonaQuery(id));
        }

        // POST api/<PersonasController>
        [HttpPost]
        public async Task<ActionResult<PersonaCommandDto>> Post([FromBody] CreatePersonaCommand createPersona)
        {
            var result = await _personaService.AddAsync(createPersona);

            return CreatedAtAction("GetByIdPersona", new { id = result.IdPersona }, result);
        }

        // PUT api/<PersonasController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdatePersonaCommand updatePersona)
        {
            if (id != updatePersona.IdPersona)
            {
                return BadRequest();
            }

            await _personaService.UpdateAsync(updatePersona);

            return NoContent();
        }

        // DELETE api/<PersonasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _personaService.DeleteAsync(new DeletePersonaCommand(id));

            return NoContent();
        }
    }


44- Agregar Cors en archivo Program.cs después de builder:

builder.Services.AddCors(options =>
{
    options.AddPolicy("Personas.CORS", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


app.UseCors("Personas.CORS");

45- Agregar ConnectionStrings en archivo Program.cs después de builder:

using CRUD.DDD.BackEnd.Net.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext") ?? throw new InvalidOperationException("Connection string 'DataContext' not found.")));


46- Realizamos inyección de dependencias en archivo Program.cs después de builder:

builder.Services.AddTransient<IPersonaRepository, PersonaRepository>();
builder.Services.AddTransient<IPersonaService, PersonaService>();
builder.Services.AddTransient<CreatePersonaCommandHandler>();
builder.Services.AddTransient<DeletePersonaCommandHandler>();
builder.Services.AddTransient<UpdatePersonaCommandHandler>();
builder.Services.AddTransient(x => new GetAllPersonaQueryHandler(builder.Configuration.GetConnectionString("DataContext")));
builder.Services.AddTransient(x => new GetByIdPersonaQueryHandler(builder.Configuration.GetConnectionString("DataContext")));


47- Ejecutar y probar