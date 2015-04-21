using ACME.Maintenance.Domain.DTO;
using ACME.Maintenance.Domain.Interfaces;

namespace ACME.Maintenance.Domain
{
  public class PartService
  {
    private readonly IPartServiceRepository _partServiceRepository;

    public PartService(IPartServiceRepository partServiceRepository)
    {
      _partServiceRepository = partServiceRepository;
    }

    public Part GetById(string partId)
    {
      var partDto = _partServiceRepository.GetById(partId);
      var part = AutoMapper.Mapper.Map<PartDto, Part>(partDto);
      return part;
    }

  }
}
