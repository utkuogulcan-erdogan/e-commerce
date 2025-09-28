using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstract
{
    public interface IBasketLineService
    {
        Task<IResult> AddProductToBasketAsync(Guid userId, Guid productId, int quantity);
        Task<IResult> RemoveProductFromBasketAsync(Guid userId, Guid productId);
        Task<IResult> UpdateProductQuantityAsync(Guid userId, Guid productId, int quantity);
        //Task<IDataResult<List<BasketLineDetailDto>>> GetBasketLinesByBasketIdAsync(Guid basketId);
    }
}
