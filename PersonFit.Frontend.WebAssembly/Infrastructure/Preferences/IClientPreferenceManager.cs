using MudBlazor;

namespace PersonFit.Frontend.WebAssembly.Infrastructure.Preferences;

public interface IClientPreferenceManager : IPreferenceManager
{
    Task<MudTheme> GetCurrentThemeAsync();

    Task<bool> ToggleDarkModeAsync();

    Task<bool> ToggleDrawerAsync();

    Task<bool> ToggleLayoutDirectionAsync();
    Task<bool> IsFirstVisit();
    Task ChangeFirstVisitAsync(bool isFirstVisit);
}