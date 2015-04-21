using ACME.Maintenance.Domain.DTO;

namespace ACME.Maintenance.Domain.Interfaces
{
  public interface IPartServiceRepository
  {
    PartDto GetById(string partId);
  }
}