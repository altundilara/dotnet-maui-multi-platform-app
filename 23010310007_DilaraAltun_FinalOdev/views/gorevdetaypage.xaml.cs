using _23010310007_DilaraAltun_FinalOdev.VeriModelleri;
using _23010310007_DilaraAltun_FinalOdev.services;

namespace _23010310007_DilaraAltun_FinalOdev.views;

public partial class gorevdetaypage : ContentPage
{
    TodoService service = new TodoService();
    Gorev _gorev;

    public gorevdetaypage(Gorev gorev)
    {
        InitializeComponent();

        if (gorev == null)
        {
            _gorev = new Gorev
            {
                TarihSaat = DateTime.Now
            };
        }
        else
        {
            _gorev = gorev;
            BaslikEntry.Text = gorev.Baslik;
            DetayEditor.Text = gorev.Detay;
            TarihPicker.Date = gorev.TarihSaat.Date;
            SaatPicker.Time = gorev.TarihSaat.TimeOfDay;
            YapildiSwitch.IsToggled = gorev.YapildiMi;
        }
    }

    private async void OnKaydetClicked(object sender, EventArgs e)
    {
        _gorev.Baslik = BaslikEntry.Text;
        _gorev.Detay = DetayEditor.Text;
        _gorev.TarihSaat = TarihPicker.Date + SaatPicker.Time;
        _gorev.YapildiMi = YapildiSwitch.IsToggled;

        if (string.IsNullOrEmpty(_gorev.Id))
            await service.GorevEkle(_gorev);
        else
            await service.GorevGuncelle(_gorev);

        await Navigation.PopAsync();
    }
}