using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Services.Configuration.Models
{
    public class ApiSettings
    {
        public JwtSettings JwtSettings { get; set; }
        public DatabaseSettings DatabaseSettings { get; set; }
    }
}
