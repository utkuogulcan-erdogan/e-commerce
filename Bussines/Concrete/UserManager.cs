using Bussiness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal) { 
            _userDal = userDal;
        }

        public async Task<IResult> Add(User user)
        {
            await _userDal.AddAsync(user);
            return new SuccessResult("User added successfully.");
        }

        public async Task<IResult> Delete(Guid id)
        {
            await _userDal.DeleteAsync(id);
            return new SuccessResult("User deleted successfully.");
        }

        public async Task<IDataResult<List<User>>> GetAll()
        {
            var response = await _userDal.GetAllAsync();
            return new SuccessDataResult<List<User>>(response, "Users listed successfully.");
        }

        public async Task<IDataResult<User>> GetByMail(string email)
        {
            return new SuccessDataResult<User>(await _userDal.GetAsync(u => u.Email == email), "User retrieved successfully.");
        }

        public async Task<IResult> Update(User user)
        {
            await _userDal.UpdateAsync(user);
            return new SuccessResult("User updated successfully.");
        }
    }
}
