using backend.Models;
using Microsoft.EntityFrameworkCore;

using backend.Services.Contrato;
using backend.Services.Implementacion;
using AutoMapper;
using backend.DTO_s;
using backend.Utilidades;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbempleadoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

//implementar la inyeccion de los servicios
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
//referencia de automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region PETICIONES API REST

app.MapGet("departamento/lista", async (
    IDepartamentoService _departamentoService,
    IMapper _mapper
    ) =>
{
    List<Departamento>listaDepartamento = await _departamentoService.GetList();
    List<DepartamentoDTO> listaDepartamentoDTO = _mapper.Map<List<DepartamentoDTO>>(listaDepartamento);

    if (listaDepartamentoDTO.Count > 0)
    {
        return Results.Ok(listaDepartamentoDTO);
    }
    else
    {
        return Results.NotFound();
    }
});

#endregion

app.Run();
