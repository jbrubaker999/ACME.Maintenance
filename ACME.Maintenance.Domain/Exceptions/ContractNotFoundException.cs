using System;
using System.Runtime.Serialization;

namespace ACME.Maintenance.Domain.Exceptions
{
  public class ContractNotFoundException : Exception, ISerializable
  {
	public ContractNotFoundException() 
	  : base() { }

	public ContractNotFoundException(string message) 
	  : base(message) { }

	public ContractNotFoundException(string message, Exception inner) 
	  : base(message, inner) { }

	protected ContractNotFoundException(SerializationInfo info, StreamingContext context) 
	  : base(info, context) { }
  }
}
