using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChizhWPF.DTO
{
    public partial class UserDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Password { get; set; }

        public string? Weight { get; set; }

        public string? Height { get; set; }
    }
}
