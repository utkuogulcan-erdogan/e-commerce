using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstract
{
    public interface IBasketService
    {
        Task<IDataResult<List<BasketDisplayDto>>> GetAllBasketsDetailedAsync();
        Task<IDataResult<BasketDisplayDto>> GetDetailedBasketByUserIdAsync(Guid id);
        Task<IDataResult<BasketDisplayDto>> GetDetailedBasketByIdAsync(Guid id);
        Task<IResult> Add(Guid userId);
    }
}
