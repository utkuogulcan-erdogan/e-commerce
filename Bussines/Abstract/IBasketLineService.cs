using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bussiness.Abstract
{
    public interface IBasketLineService
    {
        Task<IResult> AddProductToBasketAsync(Guid userId, Guid productId, int quantity, CancellationToken cancellationToken = default);
        Task<IResult> RemoveProductFromBasketAsync(Guid userId, Guid productId, CancellationToken cancellationToken = default);
        Task<IResult> UpdateProductQuantityAsync(Guid userId, Guid productId, int quantity, CancellationToken cancellationToken = default);
        //Task<IDataResult<List<BasketLineDetailDto>>> GetBasketLinesByBasketIdAsync(Guid basketId);
    }
}
