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

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        Task<List<ProductDisplayDto>> GetAllProductsAsync(CancellationToken cancellationToken = default);
        Task<Product> GetProductAsync(ISpecification<Product> specification, CancellationToken cancellationToken = default);
    }
}
