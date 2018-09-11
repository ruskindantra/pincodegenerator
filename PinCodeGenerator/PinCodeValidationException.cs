using System;
using System.Runtime.Serialization;

namespace PinCodeGenerator
{
    [Serializable]
    public class PinCodeValidationException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public PinCodeValidationException()
        {
        }

        public PinCodeValidationException(string message) : base(message)
        {
        }

        public PinCodeValidationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected PinCodeValidationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}