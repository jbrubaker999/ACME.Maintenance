using System;
using System.Collections.Generic;
using ACME.Maintenance.Domain.DTO;
using ACME.Maintenance.Domain.Enums;
using ACME.Maintenance.Domain.Interfaces;
using ACME.Maintenance.Domain.Exceptions;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACME.Maintenance.Domain.Test
{
  [TestClass]
  public class OrderServiceTest
  {
	  private IContractRepository _contractRepository;
	  private ContractService _contractService;

    private IPartServiceRepository _partServiceRepository;

	  private const string ValidContractId = "CONTRACTID";
	  private const string ExpiredContractId = "EXPIREDCONTRACTID";
    private const string ValidPartId = "PARTID";
    private const double ValidPartPrice = 50.0;

	  [TestInitialize]
	  public void Initalize()
	  {
	    _contractRepository = A.Fake<IContractRepository>();

	    _partServiceRepository = A.Fake<IPartServiceRepository>();

	    A.CallTo(() => _contractRepository.GetById(ValidContractId))
		    .Returns(new ContractDto
		    {
		      ContractId = ValidContractId,
		      ExpirationDate = DateTime.Now.AddDays(1)
		    });

	    A.CallTo(() => _contractRepository.GetById(ExpiredContractId))
		    .Returns(new ContractDto
		    {
		      ContractId = ExpiredContractId,
		      ExpirationDate = DateTime.Now.AddDays(-1)
		    });

	    A.CallTo(() => _partServiceRepository.GetById(ValidPartId))
        .Returns(new PartDto
          {
            PartId = ValidPartId, 
            Price = ValidPartPrice
          });

	    AutoMapper.Mapper.CreateMap<ContractDto, Contract>();
	    AutoMapper.Mapper.CreateMap<PartDto, Part>();

	    _contractService = new ContractService(_contractRepository);
	  }

	  [TestMethod]
	  public void CreateOrder_ValidContract_CreatesNewOrder()
	  {
	    // Arrange
	    var orderService = new OrderService();
	    var contractService = new ContractService(_contractRepository);
	    var contract = contractService.GetById(ValidContractId);

	    // Act
	    var newOrder = orderService.CreateOrder(contract);

	    // Assert
	    Assert.IsInstanceOfType(newOrder, typeof(Order));

	    Guid guidOut;
	    Assert.IsTrue(Guid.TryParse(newOrder.OrderId, out guidOut));

	    Assert.AreEqual(newOrder.Status, ContractStatus.New);
	    Assert.IsInstanceOfType(newOrder.Items, typeof(IReadOnlyList<OrderItem>));
	  }

    [TestMethod, ExpectedException(typeof(ExpiredContractException))]
    public void CreateOrder_ExpiredContract_ThrowsException()
    {
      // Arrange
      var orderService = new OrderService();
      var contractService = new ContractService(_contractRepository);
      var contract = contractService.GetById(ExpiredContractId);

      // Act
      var order = orderService.CreateOrder(contract);

    }

    [TestMethod]
    public void CreateOrderItem_ValidPart_CreatesOrderItem()
    {
      // Arrange
      var orderService = new OrderService();
      var contractService = new ContractService(_contractRepository);
      var contract = contractService.GetById(ValidContractId);
      var order = orderService.CreateOrder(contract);

      var partService = new PartService(_partServiceRepository);
      var part = partService.GetById(ValidPartId);

      var quantity = 1;

      // Act
      var orderItem = orderService.CreateOrderItem(part, quantity);

      // Assert
      Assert.AreEqual(orderItem.Part, part);
      Assert.AreEqual(orderItem.Quantity, quantity);
      Assert.AreEqual(orderItem.Price, ValidPartPrice);
      Assert.AreEqual(orderItem.LineTotal, quantity * ValidPartPrice);
    }

  }
}
