using PersonFit.Frontend.WebAssembly.Infrastructure.Common;
using PersonFit.Frontend.WebAssembly.Infrastructure.Theme;

namespace PersonFit.Frontend.WebAssembly.Infrastructure.Preferences;

public class ClientPreference : IPreference
{
    public bool IsFirstVisit { get; set; } = true;
    public bool IsDarkMode { get; set; }
    public bool IsRTL { get; set; }
    public bool IsDrawerOpen { get; set; }
    public string PrimaryColor { get; set; } = CustomColors.Light.Primary;
    public string SecondaryColor { get; set; } = CustomColors.Light.Secondary;
    public double BorderRadius { get; set; } = 15;
    public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "pl-PL";
    public FshTablePreference TablePreference { get; set; } = new FshTablePreference();
}
