using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroXchange.WebApi.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public string PasswordSalt { get; set; }

        public string DBConnectionString { get; set; }
    }
}
