using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroXchange.WebApi.Models
{
    public class Mail
    {
        public IDictionary<string, string> To { get; set; }

        public IDictionary<string, string> Cc { get; set; }

        public string Subject { get; set; }

        public string BodyHtml { get; set; }

        public Mail()
        {
            this.To = new Dictionary<string, string>();
        }

        public void AddToRecipient(string name, string email)
        {
            if (!this.To.Keys.Contains(email))
                this.To.Add(email, name);
        }

        public void AddCcRecipient(string name, string email)
        {
            if (this.Cc == null)
                this.Cc = new Dictionary<string, string>();
            if (!this.Cc.Keys.Contains(email))
                this.Cc.Add(email, name);
        }
    }
}
