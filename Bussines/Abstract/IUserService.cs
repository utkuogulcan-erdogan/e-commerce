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
    public interface IUserService
    {
        //IDataResult<List<OperationClaim>> GetClaims(User user);
        Task<IDataResult<UserDisplayDto>> GetByMailAsync(string email, CancellationToken cancellationToken = default);
        Task<IResult> AddAsync(UserAddDto user, CancellationToken cancellationToken = default);
        Task<IResult> UpdateAsync(Guid id, UserUpdateDto user, CancellationToken cancellationToken = default);
        Task<IResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IDataResult<List<UserDisplayDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
