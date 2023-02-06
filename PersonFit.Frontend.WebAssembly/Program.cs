using System.Globalization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using PersonFit.Frontend.WebAssembly;
using Blazored.LocalStorage;
using MudBlazor.Services;
using PersonFit.Frontend.WebAssembly.Infrastructure.Common;
using PersonFit.Frontend.WebAssembly.Infrastructure.Notifications;
using PersonFit.Frontend.WebAssembly.Infrastructure.Preferences;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources")
    .AddBlazoredLocalStorage()
    .AddMudServices(configuration =>
    {
        configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
        configuration.SnackbarConfiguration.HideTransitionDuration = 100;
        configuration.SnackbarConfiguration.ShowTransitionDuration = 100;
        configuration.SnackbarConfiguration.VisibleStateDuration = 3000;
        configuration.SnackbarConfiguration.ShowCloseIcon = false;
    })
    .AutoRegisterInterfaces<IAppService>()
    .AddNotifications();

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes
        .Add("https://personfit.onmicrosoft.com/6d9b6497-15fc-4aa6-8e23-0525b5229548/user_impersonation");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
    options.ProviderOptions.LoginMode = "redirect";
});

builder.Services.AddLocalization();

var host = builder.Build();

var storageService = host.Services.GetRequiredService<IClientPreferenceManager>();
{
    CultureInfo culture;
    if (await storageService.GetPreference() is ClientPreference preference)
        culture = new CultureInfo(preference.LanguageCode);
    else
        culture = new CultureInfo(LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US");
    CultureInfo.DefaultThreadCurrentCulture = culture;
    CultureInfo.DefaultThreadCurrentUICulture = culture;
}

await host.RunAsync();
