using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YuGiOhCards.Models
{
    public class Kampanya
    {
        public int Id { get; set; }

        public string Ad { get; set; }
        public DateTime Baslangic { get; set; }

        public DateTime Bitis { get; set; }

        public double IndirimOran { get; set; }

        public double MinimumDeger { get; set; }
    }
}