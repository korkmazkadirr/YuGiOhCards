using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YuGiOhCards.Models
{
    public class Kategori
    {

        public int Id { get; set; }

        public string Ad { get; set; }

        public bool Durum { get; set; } = true;
    }
}
