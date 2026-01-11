using _23010310007_DilaraAltun_FinalOdev.VeriModelleri;
using System.Text.RegularExpressions;
namespace _23010310007_DilaraAltun_FinalOdev.views;
using Microsoft.Maui.ApplicationModel;
public partial class haberdetaypage : ContentPage
{
    Haber _haber;
    public haberdetaypage(Haber haber)
    {
        InitializeComponent();

        if (haber != null)
        {
            BaslikLabel.Text = haber.title;
            DetayLabel.Text = Regex.Replace(
                haber.description,
                "<img[^>]*>",
                "",
                RegexOptions.IgnoreCase
            );

            HaberResim.Source = haber.ImageSafe;
        }
    }
    private async void OnPaylasClicked(object sender, EventArgs e)
    {
        try
        {
            await Clipboard.SetTextAsync(_haber?.link ?? "https://www.trthaber.com");

            await DisplayAlert(
                "Kopyalandý",
                "Haber baðlantýsý panoya kopyalandý.",
                "Tamam"
            );
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Hata",
                "Paylaþma iþlemi çalýþtý ama pano eriþimi engellendi.",
                "Tamam"
            );
        }
    }
}
