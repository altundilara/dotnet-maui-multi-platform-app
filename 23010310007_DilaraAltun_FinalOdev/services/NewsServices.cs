using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using _23010310007_DilaraAltun_FinalOdev.VeriModelleri;

namespace _23010310007_DilaraAltun_FinalOdev.services
{
    public class NewsServices
    {
        private HttpClient _httpClient = new HttpClient();

        public async Task<ObservableCollection<Haber>> GetNewsAsync(string rssUrl)
        {
            try
            {
                string url =
                    $"https://api.rss2json.com/v1/api.json?rss_url={rssUrl}";

                using var client = new HttpClient();
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<NewsResponse>(content);
                    return new ObservableCollection<Haber>(result.items);
                }
            }
            catch { }

            return new ObservableCollection<Haber>();
        }
    }
}
    