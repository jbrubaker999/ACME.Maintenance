using System;
using System.Runtime.Serialization;

namespace ACME.Maintenance.Domain.Exceptions
{
  public class ExpiredContractException : Exception, ISerializable
  {
    public ExpiredContractException() 
      : base() {}

    public ExpiredContractException(string message) 
      : base(message) { }

    public ExpiredContractException(string message, Exception inner) 
      : base(message, inner) { }

    public ExpiredContractException(SerializationInfo info, StreamingContext context) 
      : base(info, context) { }

  }
}
