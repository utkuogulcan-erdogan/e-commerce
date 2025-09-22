using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstract
{
    public interface IBasketService
    {
        Task<IDataResult<List<Basket>>> GetAll();
        Task<IDataResult<List<Basket>>> GetAllBasketsDetailedAsync();
        Task<IDataResult<Basket>> GetDetailedBasketByUserIdAsync(Guid id);
        Task<IDataResult<Basket>> GetById(Guid id);
        Task<IDataResult<Basket>> GetByUserId(Guid userId);
        Task<IResult> Add(Guid userId);
        Task<IResult> Update(Basket basket);
        Task<IResult> Delete(Guid id);
    }
}
