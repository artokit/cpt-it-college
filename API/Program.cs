using Application.Extensions;
using FluentMigrator.Runner;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["ConnectionStrings:Database"];
builder.Services.AddMigrations(connectionString);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDapper();
builder.Services.AddRepositories();
builder.Services.AddServices();

var app = builder.Build();
var serviceProvider = app.Services.CreateScope().ServiceProvider;
var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
runner.MigrateUp();

app.MapControllers();
app.MapSwagger();
app.UseSwaggerUI();

app.Run();
