using _23010310007_DilaraAltun_FinalOdev.services;
using _23010310007_DilaraAltun_FinalOdev.VeriModelleri;
using System.Collections.ObjectModel;

namespace _23010310007_DilaraAltun_FinalOdev.views;

public partial class yapilacaklarpage : ContentPage
{
    TodoService service = new TodoService();

    public yapilacaklarpage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        TodoList.ItemsSource = await service.GorevleriGetir();
    }

    private async void OnEkleClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new gorevdetaypage(null));
    }

    private async void OnGorevSecildi(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Gorev secilenGorev)
        {
            ((CollectionView)sender).SelectedItem = null;
            await Navigation.PushAsync(new gorevdetaypage(secilenGorev));
        }
    }

    private async void OnSilClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Gorev gorev)
        {
            bool cevap = await DisplayAlert(
                "Görevi Sil",
                $"'{gorev.Baslik}' görevini silmek istediðinize emin misiniz?",
                "Evet",
                "Hayýr"
            );

            if (cevap)
            {
                await service.GorevSil(gorev.Id);
                TodoList.ItemsSource = await service.GorevleriGetir();
            }
        }
    }
}

