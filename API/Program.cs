using API.Extensions;
using API.Middlewares;
using Application.Extensions;
using FluentMigrator.Runner;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
    DotNetEnv.Env.Load("../.env");
}
var connectionString = builder.Configuration["ConnectionStrings:Database"];

builder.Services.AddJwtTokenBearer();
// builder.Services.AddCors(options =>
// {
 //   options.AddDefaultPolicy(builder =>
//    {
//        builder.AllowAnyOrigin()
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//    });
// });

builder.Services.AddSwaggerWithAuth();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddMigrations(connectionString);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDapper();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddMinio();

var app = builder.Build();
var serviceProvider = app.Services.CreateScope().ServiceProvider;
var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
runner.MigrateUp();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();
app.MapSwagger();
app.UseSwaggerUI();

app.UseCors(x => x.AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowCredentials());
app.Run();
