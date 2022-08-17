using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace YuGiOhCards.Models
{
    public class Urun
    {
        public int Id { get; set; }

        public string Ad { get; set; }

        public double Fiyat { get; set; }

        public double Miktar { get; set; }

        public string Aciklama { get; set; }

        public Birim Birim { get; set; }

        public string UretimYeri { get; set; }

        public int? KategoriId { get; set; }
        [ForeignKey("KategoriId")]
        public Kategori Kategori { get; set; }

        public ICollection<Foto> Foto { get; set; }
    }


    public enum Birim
    {
        Adet 
        // ürün türü kart temelinden çıkarsa birim miktarı artabilir
    }
}
