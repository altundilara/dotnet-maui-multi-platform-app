using _23010310007_DilaraAltun_FinalOdev.VeriModelleri; 
using Newtonsoft.Json; 
using System.Net.Http.Json;

namespace _23010310007_DilaraAltun_FinalOdev.views;

public partial class kurlarpage : ContentPage
{
    private readonly string api_url = "https://hasanadiguzel.com.tr/api/kurgetir";

    public kurlarpage()
    {
        InitializeComponent();
        GetKurlar();
    }

    private async void GetKurlar()
    {
        LoadingIndicator.IsRunning = true;
        LoadingIndicator.IsVisible = true;

        try
        {
            using var client = new HttpClient();
            
            client.DefaultRequestHeaders.Add("User-Agent", "MauiApp");

            var response = await client.GetStringAsync(api_url);

        
            var result = JsonConvert.DeserializeObject<DovizResponse>(response);

            if (result?.TCMB_AnlikKurBilgileri != null)
            {
                var kurlarListesi = new List<Doviz>();

                foreach (var item in result.TCMB_AnlikKurBilgileri)
                {
                    kurlarListesi.Add(new Doviz
                    {
                        Isim = item.Isim,
                        Alis = item.ForexBuying,  
                        Satis = item.ForexSelling,
                        Fark = "---"              
                    });
                }

                KurlarList.ItemsSource = kurlarListesi;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Kurlar çekilemedi: " + ex.Message, "Tamam");
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
        }
    }
}


public class DovizResponse
{
    public List<TCMB_AnlikKurBilgileri> TCMB_AnlikKurBilgileri { get; set; }
}

public class TCMB_AnlikKurBilgileri
{
    public string Isim { get; set; }
    public string ForexBuying { get; set; }  
    public string ForexSelling { get; set; } 
}