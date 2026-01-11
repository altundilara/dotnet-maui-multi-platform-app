using _23010310007_DilaraAltun_FinalOdev.VeriModelleri;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace _23010310007_DilaraAltun_FinalOdev.Services
{
    public class MysqlService
    {
        // ÖNEMLİ: Emülatör için 10.0.2.2, kendi bilgisayarın için localhost veya IP
        string connectionString = "Server=10.0.2.2;Database=dilara_db;Uid=root;Pwd=1234;Port=3306;";

        // 1. GÖREVLERİ LİSTELE
        public async Task<ObservableCollection<Gorev>> GetGorevler()
        {
            var liste = new ObservableCollection<Gorev>();
            try
            {
                using var conn = new MySqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new MySqlCommand("SELECT * FROM gorevler", conn);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    liste.Add(new Gorev
                    {
                        Id = reader.GetInt32("Id"),
                        Baslik = reader.GetString("Baslik"),
                        Detay = reader.GetString("Detay"),
                        Tarih = (DateTime)reader["Tarih"],
                        Saat = (TimeSpan)reader["Saat"],
                        IsDone = reader.GetBoolean("IsDone")
                    });
                }
            }
            catch { }
            return liste;
        }

        // 2. GÖREV SİL
        public async Task<bool> GorevSil(int id)
        {
            try
            {
                using var conn = new MySqlConnection(connectionString);
                await conn.OpenAsync();
                using var cmd = new MySqlCommand("DELETE FROM gorevler WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                return await cmd.ExecuteNonQueryAsync() > 0;
            }
            catch { return false; }
        }

        // 3. HATAYI ÇÖZEN METOT: GÖREV EKLE (Bayram Hoca'nın KisiEkle mantığı)
        public async Task<bool> GorevEkle(Gorev g)
        {
            try
            {
                using var conn = new MySqlConnection(connectionString);
                await conn.OpenAsync();
                // Veritabanındaki tablo ve sütun isimlerinin bunlarla aynı olduğundan emin ol
                string sql = "INSERT INTO gorevler (Baslik, Detay, Tarih, Saat, IsDone) VALUES (@b, @d, @t, @s, @i)";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@b", g.Baslik);
                cmd.Parameters.AddWithValue("@d", g.Detay);
                cmd.Parameters.AddWithValue("@t", g.Tarih);
                cmd.Parameters.AddWithValue("@s", g.Saat);
                cmd.Parameters.AddWithValue("@i", g.IsDone);

                return await cmd.ExecuteNonQueryAsync() > 0;
            }
            catch (Exception ex)
            {
                // Hata mesajını görmek istersen: Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}