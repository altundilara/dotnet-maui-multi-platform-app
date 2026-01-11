using _23010310007_DilaraAltun_FinalOdev.VeriModelleri;
using Firebase.Database;
using Firebase.Database.Query;

public class TodoService
{
    FirebaseClient firebase = new FirebaseClient(
        "https://final-odev-134ab-default-rtdb.firebaseio.com/"
    );

    public async Task<List<Gorev>> GorevleriGetir()
    {
        var data = await firebase
            .Child("gorevler")
            .OnceAsync<Gorev>();

        return data.Select(x =>
        {
            x.Object.Id = x.Key;
            return x.Object;
        }).ToList();
    }

    public async Task GorevEkle(Gorev gorev)
    {
        await firebase
            .Child("gorevler")
            .PostAsync(gorev);
    }

    public async Task GorevGuncelle(Gorev gorev)
    {
        await firebase
            .Child("gorevler")
            .Child(gorev.Id)
            .PutAsync(gorev);
    }

    public async Task GorevSil(string id)
    {
        await firebase
            .Child("gorevler")
            .Child(id)
            .DeleteAsync();
    }
}