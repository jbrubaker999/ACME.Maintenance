using System;
using ACME.Maintenance.Domain.Enums;
using ACME.Maintenance.Domain.Exceptions;

namespace ACME.Maintenance.Domain
{
	public class OrderService
	{
		public Order CreateOrder(Contract contract)
		{
      if (DateTime.Now > contract.ExpirationDate)
        throw new ExpiredContractException();

		  var order = new Order
		    {
		      OrderId = Guid.NewGuid().ToString(), 
          Status = ContractStatus.New
		    };
		  return order;
		}

	  public OrderItem CreateOrderItem(Part part, int quantity)
	  {
	    var orderItem = new OrderItem
	      {
	        Part = part,
	        Quantity = quantity,
	        Price = part.Price,
	        LineTotal = quantity*part.Price
	      };

	    return orderItem;
	  }
	}
}