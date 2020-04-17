using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.ViewModel.Lookup
{
    public class CurrencyLookupItem
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string CurrencyGlyphicon { get; set; }

        public static List<CurrencyLookupItem> GetLookup()
        {
            var lookup = new List<CurrencyLookupItem>();
            lookup.Add(new CurrencyLookupItem { CurrencyId = 1, Name = "USD", CurrencyGlyphicon = "glyphicon glyphicon-usd" });
            lookup.Add(new CurrencyLookupItem { CurrencyId = 2, Name = "Pound", CurrencyGlyphicon = "glyphicon glyphicon-gbp" });
            lookup.Add(new CurrencyLookupItem { CurrencyId = 3, Name = "Euro", CurrencyGlyphicon = "glyphicon glyphicon-euro" });
            return lookup;
        }

        public static string GetLookupTextByIcon(string currenyGlyphicon)
        {
            var items = GetLookup();
            return items.Where(p => p.CurrencyGlyphicon == currenyGlyphicon).Select(p => p.Name).FirstOrDefault();
        }

        public static string GetLookupTextById(int id)
        {
            var items = GetLookup();
            return items.Where(p => p.CurrencyId == id).Select(p => p.Name).FirstOrDefault();
        }
    }
}
