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
    public interface IProductService
    {
        Task<IDataResult <List<Product>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IDataResult<List<ProductDisplayDto>>> GetAllProductsAsync(CancellationToken cancellationToken = default);
        Task<IDataResult<Product>> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<IResult> AddAsync(ProductAddDto productAddDto, CancellationToken cancellationToken = default);
        Task<IResult> UpdateAsync(Guid id, ProductUpdateDto productUpdateDto, CancellationToken cancellationToken = default);
        Task<IResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
