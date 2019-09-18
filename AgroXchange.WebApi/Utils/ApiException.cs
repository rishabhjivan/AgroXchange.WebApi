using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroXchange.WebApi.Utils
{
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message) { }
    }
}
