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
    public interface IUserService
    {
        //IDataResult<List<OperationClaim>> GetClaims(User user);
        Task<IDataResult<UserDisplayDto>> GetByMailAsync(string email);
        Task<IResult> AddAsync(UserAddDto user);
        Task<IResult> UpdateAsync(Guid id, UserUpdateDto user);
        Task<IResult> DeleteAsync(Guid id);
        Task<IDataResult<List<UserDisplayDto>>> GetAllAsync();
    }
}
