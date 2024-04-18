using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChizhWPF.DTO
{
    public partial class CheckDTO
    {
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public int? IdUser { get; set; }

        public decimal? Weight1 { get; set; }
    }
}
