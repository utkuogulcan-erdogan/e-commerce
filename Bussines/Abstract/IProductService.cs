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
    public interface IProductService
    {
        Task<IDataResult <List<Product>>> GetAllAsync();
        Task<IDataResult<List<ProductDisplayDto>>> GetAllProductsAsync();
        Task<IDataResult<Product>> GetProductByIdAsync(Guid productId);
        Task<IResult> AddAsync(ProductAddDto productAddDto);
        Task<IResult> UpdateAsync(Guid id,ProductUpdateDto productUpdateDto);
        Task<IResult> DeleteAsync(Guid id);
    }
}
