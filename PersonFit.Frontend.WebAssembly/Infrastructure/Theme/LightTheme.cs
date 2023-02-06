using MudBlazor;

namespace PersonFit.Frontend.WebAssembly.Infrastructure.Theme;

public class LightTheme : MudTheme
{
    public LightTheme()
    {
        Palette = new Palette()
        {
            Primary = CustomColors.Light.Primary,
            Secondary = CustomColors.Light.Secondary,
            Background = CustomColors.Light.Background,
            Surface = CustomColors.Light.Surface,
            AppbarBackground = CustomColors.Light.AppbarBackground,
            AppbarText = CustomColors.Light.AppbarText,
            DrawerBackground = CustomColors.Light.DrawerBackground,
            DrawerText = "rgba(0,0,0, 0.7)",
            LinesDefault = CustomColors.Light.LinesDefault,
            Success = CustomColors.Light.Success,
            Error = CustomColors.Light.Error,
            TableLines = "#e0e0e029",
            OverlayDark = "hsl(0deg 0% 0% / 75%)"
        };
        LayoutProperties = new LayoutProperties()
        {
            DefaultBorderRadius = "15px"
        };

        Typography = CustomTypography.FSHTypography;
        Shadows = new Shadow();
        ZIndex = new ZIndex() { Drawer = 1300 };
    }
}