using System;
using System.Net;
using System.Runtime.Serialization;

namespace B2CGraphClient
{
    [Serializable]
    public class B2CGraphApiException : Exception
    {
        public B2CGraphApiException()
        {
        }

        public B2CGraphApiException(string message)
            : base(message)
        {
        }

        public B2CGraphApiException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected B2CGraphApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public HttpStatusCode StatusCode { get; set; }
    }
}