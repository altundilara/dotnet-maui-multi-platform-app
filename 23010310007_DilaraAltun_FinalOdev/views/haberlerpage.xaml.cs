using _23010310007_DilaraAltun_FinalOdev.VeriModelleri;
using System.Net.Http.Json;
using _23010310007_DilaraAltun_FinalOdev.services;
namespace _23010310007_DilaraAltun_FinalOdev.views;

public partial class haberlerpage : ContentPage
{
    NewsServices _newsService = new NewsServices();

    
    Dictionary<string, string> kategoriler = new()
    {
        { "Manþet", "https://www.trthaber.com/manset_articles.rss" },
        { "Son Dakika", "https://www.trthaber.com/sondakika_articles.rss" },
        { "Gündem", "https://www.trthaber.com/gundem_articles.rss" },
        { "Ekonomi", "https://www.trthaber.com/ekonomi_articles.rss" },
        { "Spor", "https://www.trthaber.com/spor_articles.rss" }
    };

    public haberlerpage()
    {
        InitializeComponent();

        
        KategoriPicker.ItemsSource = kategoriler.Keys.ToList();
        KategoriPicker.SelectedIndex = 0; 
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await HaberleriYukle(kategoriler["Manþet"]);
    }

    
    private async void OnKategoriDegisti(object sender, EventArgs e)
    {
        if (KategoriPicker.SelectedIndex == -1) return;

        string secilenKategori = KategoriPicker.SelectedItem.ToString();
        string rssUrl = kategoriler[secilenKategori];

        await HaberleriYukle(rssUrl);
    }

    
    private async Task HaberleriYukle(string rssUrl)
    {
        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;

        HaberlerList.ItemsSource =
            await _newsService.GetNewsAsync(rssUrl);

        LoadingIndicator.IsRunning = false;
        LoadingIndicator.IsVisible = false;
    }

    
    private async void OnHaberSecildi(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Haber secilenHaber)
        {
            ((CollectionView)sender).SelectedItem = null;
            await Navigation.PushAsync(new haberdetaypage(secilenHaber));
        }
    }
}