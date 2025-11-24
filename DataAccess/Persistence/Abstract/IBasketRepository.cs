using Core.DataAccess;
using Core.Specifications;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Abstract
{
    public interface IBasketRepository : IEntityRepository<Basket>
    {
        Task<List<BasketDisplayDto>> GetAllBasketsDetailedAsync(CancellationToken cancellationToken = default);
        Task<BasketDisplayDto> GetDetailedBasketByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<BasketDisplayDto> GetDetailedBasketAsync(ISpecification<Basket> specification, CancellationToken cancellationToken = default);
    }
}
