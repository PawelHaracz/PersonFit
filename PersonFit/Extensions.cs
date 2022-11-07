namespace PersonFit;
using Application;

internal static class Extensions
{
    public static WebApplication AddWeb(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddEnvironmentVariables();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.RegisterApplication();

        return builder.Build();
    }

    public static WebApplication UseWeb(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        if (app.Environment.IsDevelopment())
        {
            
        }
        app.UseApplication();
        app.MapGet("/", () =>
        {
            var world = "world";
            using var scope = app.Logger.BeginScope("ble ble {@World}", world);
            var hello = "hello";
            app.Logger.LogInformation("message {@Hello}", hello);

            return string.Concat(hello, " ", world);
        });
        
        return app;
    }
}