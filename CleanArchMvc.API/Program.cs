using Microsoft.EntityFrameworkCore;
using ClearArchMvc.Infra.Ioc;
using ClearArchMvc.Infra.Data.Context;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Configuração da string de conexão
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Console.WriteLine($"Connection String: {connectionString}");

// Registro do DbContext no contêiner de serviços
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Adiciona o servicos configurados em Dependecy Injection
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();


app.Run();

