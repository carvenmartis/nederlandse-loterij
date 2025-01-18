using NederlandseLoterij.API.Middelwares;
using NederlandseLoterij.Application;
using NederlandseLoterij.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new ArgumentNullException("DefaultConnection", "Connection string cannot be null.");

builder.Services.AddInfrastructure(connectionString);
builder.Services.AddHealthChecks();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);  // HTTP
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/api/health");
app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();