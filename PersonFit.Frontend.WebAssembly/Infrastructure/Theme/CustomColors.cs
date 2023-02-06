using MudBlazor;

namespace PersonFit.Frontend.WebAssembly.Infrastructure.Theme;

public static class CustomColors
{
    public static readonly List<string> ThemeColors = new()
    {
        Light.Primary,
        Colors.Blue.Default,
        Colors.BlueGrey.Default,
        Colors.Purple.Default,
        Colors.Orange.Default,
        Colors.Red.Default,
        Colors.Amber.Default,
        Colors.DeepPurple.Default,
        Colors.Pink.Default,
        Colors.Indigo.Default,
        Colors.LightBlue.Default,
        Colors.Cyan.Default,
    };

    public static class Light
    {
        public const string Primary = "#308eb0";
        public const string Secondary = "#e64b5d";
        public const string Success = "#82b441";
        public const string Error = "#c24242";
        //public const string Background = "#EBEDF4";
        public const string Background = "#F2F5FA";
        public const string Surface = "#FFFFFF";
        public const string AppbarBackground = "#FFFFFF";
        public const string DrawerBackground = "#FFFFFF";
        public const string AppbarText = "#6e6e6e";
        public const string LinesDefault = "#dbdbdb";
    }

    public static class Dark
    {
        public const string Primary = "#308eb0";
        public const string Secondary = "#e64b5d";
        public const string Success = "#82b441";
        public const string Error = "#c24242";
        //public const string Background = "#1b1f22";
        //public const string AppbarBackground = "#1b1f22";
        //public const string DrawerBackground = "#121212";
        public const string Background = "#003644";
        public const string AppbarBackground = "#003644";
        public const string DrawerBackground = "#00222b";
        //public const string Surface = "#373740";
        public const string Surface = "#00222b";
        public const string Disabled = "#545454";
    }
}