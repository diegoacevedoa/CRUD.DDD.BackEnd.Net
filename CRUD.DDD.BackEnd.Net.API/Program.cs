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
