using ACME.Maintenance.Domain.DTO;

namespace ACME.Maintenance.Domain.Interfaces
{
  public interface IContractRepository
  {
	  ContractDto GetById(string contractId);
  }
}
