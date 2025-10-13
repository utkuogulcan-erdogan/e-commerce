using Bussiness.Abstract;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTO_s;
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
        IHashingHelper _hashingHelper;
        public UserManager(IUserDal userDal,IHashingHelper hashingHelper, IBasketDal basketDal) { 
            _userDal = userDal;
            _hashingHelper = hashingHelper;
        }

        public async Task<IResult> Add(UserAddDto user)
        {
            _hashingHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            await _userDal.AddUserAsync(user, passwordHash, passwordSalt);
            return new SuccessResult("User added successfully.");
        }

        public async Task<IResult> Delete(Guid id)
        {
            await _userDal.DeleteAsync(id);
            return new SuccessResult("User deleted successfully.");
        }

        public async Task<IDataResult<List<UserDisplayDto>>> GetAll()
        {
            var response = await _userDal.GetAllUserAsync();
            return new SuccessDataResult<List<UserDisplayDto>>(response, "Users listed successfully.");
        }

        public async Task<IDataResult<UserDisplayDto>> GetByMail(string email)
        {
            return new SuccessDataResult<UserDisplayDto>(await _userDal.GetUserAsync(email), "User retrieved successfully.");
        }

        public async Task<IResult> Update(Guid id, UserUpdateDto user)
        {
            var existingUser = await _userDal.GetAsync(u => u.Id == id);
            if (existingUser == null)
            {
                return new ErrorResult("User not found.");
            }
            if (!string.IsNullOrWhiteSpace(user.FullName))
            {
                existingUser.FullName = user.FullName;
            }
            if (!string.IsNullOrWhiteSpace(user.UserName))
            {
                existingUser.UserName = user.UserName;
            }
            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                existingUser.Email = user.Email;
            }
            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                _hashingHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                existingUser.PasswordHash = passwordHash;
                existingUser.PasswordSalt = passwordSalt;
            }
            await _userDal.UpdateAsync(existingUser);
            return new SuccessResult("User updated successfully.");
        }
    }
}
