using PersonFit.Core;
using PersonFit.Domain.Exercise.Api.Dtos;
using PersonFit.Domain.Exercise.Application.Commands;
using PersonFit.Domain.Exercise.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddInfrastructure();

var app = builder.Build();

var cts = new CancellationTokenSource();

app.UseInfrastructure();
app.UseSwagger();
app.UseSwaggerUI();
if (app.Environment.IsDevelopment())
{

}

app.MapGet("/", () =>
{
    var world = "world";
    using var scope = app.Logger.BeginScope("ble ble {@World}", world);
    var hello = "hello";
    app.Logger.LogInformation("message {@Hello}", hello);

    return string.Concat(hello, " ", world);
});

app.MapPut("/exercise", async (AddExerciseCommandDto dto, CancellationToken token, ICommandDispatcher dispatcher) =>
{
    var id = Guid.NewGuid();

    var command = new AddExerciseCommand(id, dto.Name, dto.Description, dto.Tags);
    await dispatcher.SendAsync(command, token);
    return Results.Created($"exercise/{id}", id);
});


await app.RunAsync(cts.Token);
