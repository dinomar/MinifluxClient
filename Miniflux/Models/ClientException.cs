using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Miniflux.Models
{
    public class ClientException : Exception
    {
        public int StatusCode { get; private set; }


        public ClientException()
        {
        }

        public ClientException(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public ClientException(string message) : base(message)
        {
        }

        public ClientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
