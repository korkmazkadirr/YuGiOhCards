using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YuGiOhCards.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Kategori> Kategori { get; set; }
        public IEnumerable<Urun> Urun { get; set; }
        public IEnumerable<IndirimliUrunler> IndirimliUrunler { get; set; }
    }
}
