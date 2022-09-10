using CQRS.Infras.Implementation;
using CQRS.Infras.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

builder.Services.AddControllers();

builder.Services.AddDbContext<CQRS.Db.Context.AppDbContext>(options =>
options.UseSqlServer(
              builder.Configuration.GetConnectionString("DefaultConnection")));

// Add dependencies
builder.Services.TryAddSingleton<ICommandDispatcher, CommandDispatcher>();
builder.Services.TryAddSingleton<IQueryDispatcher, QueryDispatcher>();

builder.Services.Scan(selector =>
{
    selector.FromCallingAssembly()
            .AddClasses(filter =>
            {
                filter.AssignableTo(typeof(IQueryHandler<,>));
            })
            .AsImplementedInterfaces()
            .WithSingletonLifetime();
});
builder.Services.Scan(selector =>
{
    selector.FromCallingAssembly()
            .AddClasses(filter =>
            {
                filter.AssignableTo(typeof(ICommandHandler<,>));
            })
            .AsImplementedInterfaces()
            .WithSingletonLifetime();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health");
app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseRouting();

app.Run();
