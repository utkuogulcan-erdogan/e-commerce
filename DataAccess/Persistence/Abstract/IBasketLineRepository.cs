using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Abstract
{
    public interface IBasketLineRepository : IEntityRepository<BasketLine>
    {
        Task ClearBasketLinesAsync(Guid basketId);
    }
}
