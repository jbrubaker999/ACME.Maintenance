using System;
using ACME.Maintenance.Domain.DTO;
using ACME.Maintenance.Domain.Interfaces;

namespace ACME.Maintenance.Persistence
{
    public class ContractRepository : IContractRepository
    {
	  public ContractDto GetById(string contractId)
	  {
		var contractDto = new ContractDto();

		// Stubbed ... ultimately, will go out to the
  		// database and retrieve the given Contract
		// record based on the contractId

		contractDto.ContractId = contractId;

		if (contractId == "CONTRACTID")
		{
		  contractDto.ExpirationDate = DateTime.Now.AddDays(1);			
		}
		else if (contractId == "EXPIREDCONTRACTID")
		{
		  contractDto.ExpirationDate = DateTime.Now.AddDays(-1);
		}

		return contractDto;
	  }
    }
}
