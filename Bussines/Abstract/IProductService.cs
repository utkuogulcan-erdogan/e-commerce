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
        Task<IDataResult <List<Product>>> GetAll();
        Task<IDataResult<List<ProductDisplayDto>>> GetAllProductsAsync();
        Task<IDataResult<Product>> GetById(Guid productId);
        Task<IResult> Add(ProductAddDto productAddDto);
        Task<IResult> Update(Guid id,ProductUpdateDto productUpdateDto);
        Task<IResult> Delete(Guid id);
    }
}
