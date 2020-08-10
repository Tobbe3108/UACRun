using Domain.Models;

namespace Application.Interfaces
{
  public interface ICustomerRepository
  {
    App GetApp(long guid);
    void SaveApp(App app);
  }
}