using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bussiness.Abstract
{
    public interface IBasketService
    {
        Task<IDataResult<List<BasketDisplayDto>>> GetAllBasketsDetailedAsync(CancellationToken cancellationToken = default);
        Task<IDataResult<BasketDisplayDto>> GetDetailedBasketByUserIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IDataResult<BasketDisplayDto>> GetDetailedBasketByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IResult> AddAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
