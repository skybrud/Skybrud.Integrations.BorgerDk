using System;

namespace Skybrud.Integrations.BorgerDk.Exceptions {

    public class BorgerDkException : Exception {

        public BorgerDkException(string message) : base(message) { }

        public BorgerDkException(string message, Exception innerException) : base(message, innerException) { }

    }

}