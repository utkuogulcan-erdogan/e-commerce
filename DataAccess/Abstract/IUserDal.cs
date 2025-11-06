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
    public interface IUserDal : IEntityRepository<User>
    {
        public Task<List<UserDisplayDto>> GetAllUserAsync(CancellationToken cancellationToken = default);
        public Task<UserDisplayDto> GetUserAsync(ISpecification<User> specification, CancellationToken cancellationToken = default);

    }
}
