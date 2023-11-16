using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Services.DTO
{
    public class PropertyImageDto
    {
        public Byte[] File { get; set; }
        public bool Enabled { get; set; }
        public int PropertyId { get; set; }
    }
}
