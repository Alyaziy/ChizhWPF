using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChizhWPF.DTO
{
    public partial class PozeDTO
    {
        public int Id { get; set; }

        public string? Tittle { get; set; }

        public byte[]? Image { get; set; }

        public decimal? Time { get; set; }

        public string? Description { get; set; }

        public int? IdMuscle { get; set; }
    }
}
