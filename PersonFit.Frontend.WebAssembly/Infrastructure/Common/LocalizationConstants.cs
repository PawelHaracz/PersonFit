namespace PersonFit.Frontend.WebAssembly.Infrastructure.Common;

public record LanguageCode(string Code, string FlagIcon, string DisplayName, bool IsRTL = false);

public static class LocalizationConstants
{
    public static readonly LanguageCode[] SupportedLanguages =
    {
        new("pl-PL", "fi fi-pl", "Polski" ),
        new("en-US", "fi fi-gb", "English")
    };
}