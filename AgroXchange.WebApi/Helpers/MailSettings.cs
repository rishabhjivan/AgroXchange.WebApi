using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroXchange.WebApi.Helpers
{
    public class MailSettings
    {
        public string SmtpHost { get; set; }

        public int SmtpPort { get; set; }

        public string SmtpUsername { get; set; }

        public string SmtpPassword { get; set; }

        public string FromName { get; set; }

        public string FromEmail { get; set; }
    }
}
