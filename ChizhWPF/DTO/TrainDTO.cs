using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChizhWPF.DTO
{
    public partial class TrainDTO
    {
        public int Id { get; set; }

        public string? TrTittle { get; set; }

        public string? TrDescription { get; set; }

        public decimal? TrTime { get; set; }
       
        public List<PozeDTO> Pozes { get; set; }
    }
}
