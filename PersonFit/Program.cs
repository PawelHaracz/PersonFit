using PersonFit;
var cts = new CancellationTokenSource();
await WebApplication
    .CreateBuilder(args)
    .AddWeb()
    .UseWeb()
    .RunAsync(cts.Token);
