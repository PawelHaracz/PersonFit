using PersonFit.Domain.Exercise.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.AddInfrastructure();

var app = builder.Build();

var cts = new CancellationTokenSource();

 app.UseInfrastructure();

app.MapGet("/", () =>
{
    var world = "world";
    using var scope = app.Logger.BeginScope("ble ble {@World}", world);
    var hello = "hello";
    app.Logger.LogInformation("message {@Hello}", hello);
    return string.Concat(hello, " ", world);
});

await app.RunAsync(cts.Token);
