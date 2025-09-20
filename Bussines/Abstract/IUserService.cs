using Core.Utilities.Results;
using Entities.Concrete;
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
        Task<IDataResult<User>> GetByMail(string email);
        Task<IResult> Add(User user);
        Task<IResult> Update(User user);
        Task<IResult> Delete(Guid id);
        Task<IDataResult<List<User>>> GetAll();
    }
}
