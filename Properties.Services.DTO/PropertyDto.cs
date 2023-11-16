using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Services.DTO
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public float Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
    }
}
