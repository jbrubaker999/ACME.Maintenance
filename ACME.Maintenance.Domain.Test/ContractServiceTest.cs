using System;
using ACME.Maintenance.Domain.DTO;
using ACME.Maintenance.Domain.Exceptions;
using ACME.Maintenance.Domain.Interfaces;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACME.Maintenance.Domain.Test
{
  [TestClass]
  public class ContractServiceTest
  {
	private IContractRepository _contractRepository;
	private ContractService _contractService;

	private const string ValidContractId = "CONTRACTID";
	private const string ExpiredContractId = "EXPIREDCONTRACTID";
	private const string InvalidContractId = "INVALIDCONTRACTID";

	[TestInitialize]
	public void Initalize()
	{
	  // Initialize serves as the "composition root"

	  _contractRepository = A.Fake<IContractRepository>();

      A.CallTo(() => _contractRepository.GetById(ValidContractId))
		.Returns(new ContractDto 
		   {ContractId = ValidContractId, 
		   ExpirationDate = DateTime.Now.AddDays(1)});

	  A.CallTo(() => _contractRepository.GetById(ExpiredContractId))
		.Returns(new ContractDto
		{
		  ContractId = ExpiredContractId,
		  ExpirationDate = DateTime.Now.AddDays(-1)
		});

		A.CallTo(() => _contractRepository.GetById(InvalidContractId))
		  .Throws<ContractNotFoundException>();

	  AutoMapper.Mapper.CreateMap<ContractDto, Contract>();

	  _contractService = new ContractService(_contractRepository);
	}

	[TestMethod]
	public void GetById_ValidContractId_ReturnsContract()
	{
	  // Arrange

	  // Act
	  var contract = _contractService.GetById(ValidContractId);

	  // Assert
	  Assert.IsInstanceOfType(contract, typeof(Contract));
	  Assert.IsTrue(contract.ExpirationDate > DateTime.Now);
	  Assert.AreEqual(ValidContractId, contract.ContractId);
	}

	[TestMethod]
	public void GetById_ExpiredContractId_ReturnsExpiredContract()
	{
	  var contract = _contractService.GetById(ExpiredContractId);
	  Assert.IsInstanceOfType(contract, typeof(Contract));
	  Assert.IsTrue(DateTime.Now > contract.ExpirationDate);
	  Assert.AreEqual(ExpiredContractId, contract.ContractId);
	}

	[TestMethod, ExpectedException(typeof (ContractNotFoundException))]
	public void GetById_InvalidContractId_ThrowsException()
	{
	  // Act
	  var contract = _contractService.GetById(InvalidContractId);
	}

  }
}
