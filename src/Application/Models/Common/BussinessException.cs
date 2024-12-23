using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Common
{
    public class BussinessException : Exception
    {
        public int StatusCode { get; set; }

        public string ErrorCode { get; set; } = string.Empty;

        public BussinessException(int statusCode, string errorCode, string message) : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }
}
