using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.Models.ExceptionHandling
{
    public class HttpException : Exception
    {
        public int StatusCode { get; set; }

        public HttpException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
