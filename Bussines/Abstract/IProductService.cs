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
        Task<IDataResult<List<Product>>> GetAllProductsAsync();
        Task<IDataResult<Product>> GetById(Guid productId);
        Task<IResult> Add(Product product);
        Task<IResult> Update(Product product);
        Task<IResult> Delete(Guid id);
    }
}
