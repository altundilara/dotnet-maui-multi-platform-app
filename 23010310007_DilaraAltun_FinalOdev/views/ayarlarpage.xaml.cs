namespace _23010310007_DilaraAltun_FinalOdev.views;

public partial class ayarlarpage : ContentPage
{
	public ayarlarpage()
	{
		InitializeComponent();
        ThemeSwitch.IsToggled = Application.Current.UserAppTheme == AppTheme.Dark;
    }
    private void OnThemeToggled(object sender, ToggledEventArgs e)
    {
        
        Application.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
    }
}
