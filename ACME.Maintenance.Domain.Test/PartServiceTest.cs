using ACME.Maintenance.Domain.DTO;
using ACME.Maintenance.Domain.Exceptions;
using ACME.Maintenance.Domain.Interfaces;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACME.Maintenance.Domain.Test
{
  [TestClass]
  public class PartServiceTest
  {
    private const string ValidPartId = "VALIDPARTID";
    private const string InvalidPartId = "INVALIDPARTID";
    private const double ValidPartAmount = 50.0;

    private IPartServiceRepository _partServiceRepository;

    [TestInitialize]
    public void Initialize()
    {
      _partServiceRepository = A.Fake<IPartServiceRepository>();

      A.CallTo(() => _partServiceRepository.GetById(ValidPartId))
       .Returns(new PartDto
         {
           PartId = ValidPartId, 
           Price = ValidPartAmount
         });

      A.CallTo(() => _partServiceRepository.GetById(InvalidPartId))
        .Throws<PartNotFoundException>();

      AutoMapper.Mapper.CreateMap<PartDto, Part>();
    }

    [TestMethod]
    public void GetPartById_ValidId_ReturnsPart()
    {
      var partService = new PartService(_partServiceRepository);
      var part = partService.GetById(ValidPartId);
      Assert.IsInstanceOfType(part, typeof(Part));
      Assert.AreEqual(part.PartId, ValidPartId);
      Assert.AreEqual(part.Price, ValidPartAmount);
    }

    [TestMethod, ExpectedException(typeof (PartNotFoundException))]
    public void GetPartById_InvalidPartId_ThrowsException()
    {
      var partService = new PartService(_partServiceRepository);
      var part = partService.GetById(InvalidPartId);
    }

  }
}
