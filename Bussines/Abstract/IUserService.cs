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
        Task<IDataResult<UserDisplayDto>> GetByMail(string email);
        Task<IResult> Add(UserAddDto user);
        Task<IResult> Update(Guid id, UserUpdateDto user);
        Task<IResult> Delete(Guid id);
        Task<IDataResult<List<UserDisplayDto>>> GetAll();
    }
}
