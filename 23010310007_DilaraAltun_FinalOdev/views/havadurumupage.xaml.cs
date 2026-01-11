using _23010310007_DilaraAltun_FinalOdev.VeriModelleri;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace _23010310007_DilaraAltun_FinalOdev.views;

public partial class havadurumupage : ContentPage
{
    ObservableCollection<Sehir> sehirler = new ObservableCollection<Sehir>();
    string filePath = Path.Combine(FileSystem.AppDataDirectory, "sehirler.json");

    public havadurumupage()
    {
        InitializeComponent();
        SehirleriYukle();
        SehirlerList.ItemsSource = sehirler;
    }

    private string KarakterCevir(string text)
    {
        if (string.IsNullOrEmpty(text)) return "";
        string temp = text.ToUpper()
            .Replace("Ç", "C").Replace("Ð", "G").Replace("Þ", "S")
            .Replace("Ö", "O").Replace("Ü", "U").Replace("Ý", "I").Replace("ý", "I");

        if (temp == "AFYON") temp = "AFYONKARAHISAR";
        if (temp == "KAHRAMANMARAS") temp = "K.MARAS";

        return temp;
    }

    private void OnSehirEkleClicked(object sender, EventArgs e)
    {
        string girilen = SehirEntry.Text?.Trim();
        if (!string.IsNullOrWhiteSpace(girilen))
        {
            string temiz = KarakterCevir(girilen);

            
            var yeniSehir = new Sehir { SehirAdi = temiz };

            if (!sehirler.Any(x => x.SehirAdi == temiz))
            {
                sehirler.Add(yeniSehir);
                SehirleriKaydet();
            }

            HavaDurumunuGoster(yeniSehir);
            SehirEntry.Text = string.Empty;
        }
    }

    private void HavaDurumunuGoster(Sehir sehir)
    {
        
        ResultCityLabel.Text = sehir.SehirAdi;
        TodayWeatherImage.Source = sehir.BugunUrl;
        FiveDayWeatherImage.Source = sehir.BesGunUrl;
    }

    private void OnSehirSecildi(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Sehir secilen)
        {
            HavaDurumunuGoster(secilen);
            ((CollectionView)sender).SelectedItem = null;
        }
    }

    private void SehirleriKaydet()
    {
        string json = JsonConvert.SerializeObject(sehirler);
        File.WriteAllText(filePath, json);
    }

    private void SehirleriYukle()
    {
        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                var yuklenenler = JsonConvert.DeserializeObject<List<Sehir>>(json);
                if (yuklenenler != null)
                {
                    foreach (var s in yuklenenler) sehirler.Add(s);
                }
            }
            catch { }
        }
    }
}