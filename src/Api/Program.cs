using Api.Filter;
using Infrastructure.Hubs;
using Application;
using Infrastructure;
using static Api.Extension;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddSignalR();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "127.0.0.1:6379";
    options.InstanceName = "ChatApp:";
});
builder.Services.AddProblemDetails();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.LoadOptions(builder.Configuration);
builder.Services.AddApplicationService();
builder.Services.AddInfrastructureService();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy => policy
            .SetPreflightMaxAge(TimeSpan.FromMinutes(1))
            .WithOrigins("http://localhost:3000")  // Frontend URL
            .AllowCredentials()  // Allow credentials
            .AllowAnyHeader()  // Allow all headers
            .AllowAnyMethod());  // Allow all methods
            
});

var app = builder.Build();
app.UseExceptionHandler();
// Use CORS policy
app.UseCors("AllowLocalhost");
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>("/chatHub");
app.Run();
