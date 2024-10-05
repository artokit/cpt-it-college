using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDapper();

var app = builder.Build();
app.MapControllers();

app.MapSwagger();
app.UseSwaggerUI();

app.Run();
