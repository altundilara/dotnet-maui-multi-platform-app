using _23010310007_DilaraAltun_FinalOdev.services;

namespace _23010310007_DilaraAltun_FinalOdev.views;

public partial class loginpage : ContentPage
{
    public loginpage() { InitializeComponent(); }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var email = EmailEntry?.Text?.Trim();
        var pass = PasswordEntry?.Text?.Trim();

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
        {
            await DisplayAlert("Hata", "Kayýt için kutularý doldurmalýsýn!", "Tamam");
            return;
        }

        LoadingIndicator.IsRunning = true;
        bool sonuc = await FirebaseServices.Register(email, pass);
        LoadingIndicator.IsRunning = false;

        if (sonuc) await DisplayAlert("Baþarýlý", "Hesabýn açýldý, þimdi giriþ yap.", "Tamam");
        else await DisplayAlert("Hata", "Kayýt yapýlamadý, bilgileri kontrol et.", "Tamam");
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = EmailEntry?.Text?.Trim();
        var pass = PasswordEntry?.Text?.Trim();

        bool giris = await FirebaseServices.Login(email, pass);
        if (giris) Application.Current.MainPage = new AppShell();
        else await DisplayAlert("Hata", "Email veya þifre yanlýþ!", "Tamam");
    }
}