using PersonFit.Frontend.WebAssembly.Infrastructure.Common;

namespace PersonFit.Frontend.WebAssembly.Infrastructure.Preferences;

public interface IPreferenceManager : IAppService
{
    Task SetPreference(IPreference preference);

    Task<IPreference> GetPreference();

    Task<bool> ChangeLanguageAsync(string languageCode);
}