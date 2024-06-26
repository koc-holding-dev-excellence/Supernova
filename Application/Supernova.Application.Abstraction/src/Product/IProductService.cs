
using Supernova.Domain;
using Supernova.Persistence.Repository;

namespace Supernova.Application.Abstraction;

public interface IProductService : IBaseRepository<Product>, IApplicationService
{
    List<Product> GetProducts();
}
