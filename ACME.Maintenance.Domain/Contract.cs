using System;

namespace ACME.Maintenance.Domain
{
  public class Contract
  {
	public string ContractId { get; set; }
	public DateTime ExpirationDate { get; set; }
  }
}
