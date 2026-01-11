using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _23010310007_DilaraAltun_FinalOdev.VeriModelleri
{
    public class Haber
    {
        public string title { get; set; }
        public string description { get; set; }
        public string thumbnail { get; set; }
        public string link { get; set; }

        public string ImageSafe
        {
            get
            {
                if (!string.IsNullOrEmpty(thumbnail))
                    return thumbnail.Replace("http://", "https://");

                if (!string.IsNullOrEmpty(description))
                {
                    var match = Regex.Match(description, "<img.+?src=[\"'](.+?)[\"']",
                        RegexOptions.IgnoreCase);

                    if (match.Success)
                        return match.Groups[1].Value.Replace("http://", "https://");
                }

                return "https://via.placeholder.com/600x400.png?text=TRT+Haber";
            }
        }

        
        public string DescriptionPlain =>
            Regex.Replace(description ?? "", "<img[^>]*>", "", RegexOptions.IgnoreCase);
    }

    public class NewsResponse
    {
        public List<Haber> items { get; set; }
    }
}