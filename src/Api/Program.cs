using Api.Filter;
using Application;
using static Api.Extension;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddApplicationService();
builder.Services.AddProblemDetails();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.LoadOptions(builder.Configuration);

var app = builder.Build();
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
