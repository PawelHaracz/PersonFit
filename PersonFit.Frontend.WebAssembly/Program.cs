using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Extensions;
using PersonFit.Frontend.WebAssembly;
using MudBlazor.Services;
using PersonFit.Frontend.WebAssembly.HttpClients.Exercise;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes
        .Add("https://personfit.onmicrosoft.com/6d9b6497-15fc-4aa6-8e23-0525b5229548/user_impersonation");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
    options.ProviderOptions.LoginMode = "redirect";
    options.AuthenticationPaths.LogOutSucceededPath = "";
});
builder.Services.AddMudServices();
builder.Services.AddMudExtensions();
builder.Services.AddHttpClient<IExerciseService, FakeExerciseService>();

await builder.Build().RunAsync();
