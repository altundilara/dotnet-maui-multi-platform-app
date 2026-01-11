using System;
using System.Collections.Generic;
using System.Linq;

namespace _23010310007_DilaraAltun_FinalOdev.VeriModelleri
{
    
    public class Sehir
    {
        public string SehirAdi { get; set; }

        
        public string BugunUrl => $"https://www.mgm.gov.tr/sunum/sondurum-show-2.aspx?m={Normalize(SehirAdi)}&rC=111&rZ=fff";
        public string BesGunUrl => $"https://www.mgm.gov.tr/sunum/tahmin-show-2.aspx?m={Normalize(SehirAdi)}&basla=1&bitir=5&rC=111&rZ=fff";

        
        private string Normalize(string cityName)
        {
            if (string.IsNullOrEmpty(cityName)) return "";

            cityName = cityName.ToUpper()
                               .Replace("Ç", "C")
                               .Replace("Ö", "O")
                               .Replace("Ş", "S")
                               .Replace("İ", "I")
                               .Replace("Ü", "U")
                               .Replace("Ğ", "G")
                               .Replace("ı", "I");

            
            if (cityName == "KAHRAMANMARAS") cityName = "K.MARAS";
            if (cityName == "AFYON") cityName = "AFYONKARAHISAR";

            return cityName;
        }
    }
}
