using DB1.Desafio.Api.Extensions;
using DB1.Desafio.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiConfig();

builder.Services.AddControllers();
builder.Services.AddVersioning();
builder.Services.AddSwagger();
builder.Services.AddAutoMapperSetup();
builder.Services.AddMediatRApi();

builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
