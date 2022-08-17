using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YuGiOhCards.Models
{
    public class Sepet
    {
        public int Id { get; set; }

        public int? UrunId { get; set; }
        [ForeignKey("UrunId")]
        public Urun Urun { get; set; }

        public string MusteriId { get; set; }
        [ForeignKey("MusteriId")]
        public ApplicationUser ApplicationUser { get; set; }

        public double Miktar { get; set; }

        public double Fiyat { get; set; }

        public bool SiparisOk { get; set; } = false;

        [NotMapped]
        public double Ucret
        {
            get
            {
                return Miktar * Fiyat;
            }
        }
    }
}
