using Bussiness.Abstract;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTO_s;
using Entities.Specifications;
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
        public UserManager(IUserDal userDal, IHashingHelper hashingHelper)
        {
            _userDal = userDal;
            _hashingHelper = hashingHelper;
        }

        public async Task<IResult> AddAsync(UserAddDto user)
        {
            _hashingHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var newUser = User.Create(
                user,
                passwordHash,
                passwordSalt
            );
            await _userDal.AddAsync(newUser);
            return new SuccessResult("User added successfully.");
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            await _userDal.DeleteAsync(id);
            return new SuccessResult("User deleted successfully.");
        }

        public async Task<IDataResult<List<UserDisplayDto>>> GetAllAsync()
        {
            var response = await _userDal.GetAllUserAsync();
            return new SuccessDataResult<List<UserDisplayDto>>(response, "Users listed successfully.");
        }

        public async Task<IDataResult<UserDisplayDto>> GetByMailAsync(string email)
        {
            var specification = new UserSpecification(email: email);
            return new SuccessDataResult<UserDisplayDto>(await _userDal.GetUserAsync(specification), "User retrieved successfully.");
        }

        public async Task<IResult> UpdateAsync(Guid id, UserUpdateDto user)
        {
            var existingUser = await _userDal.GetAsync(u => u.Id == id);
            if (user == null)
            {
                return new ErrorResult("User not found.");
            }
            var userPasswordHash = existingUser.PasswordHash;
            var userPasswordSalt = existingUser.PasswordSalt;
            if (!string.IsNullOrEmpty(user.Password))
            {
                _hashingHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                userPasswordHash = passwordHash;
                userPasswordSalt = passwordSalt;
            }
            var updatedUser = User.Update(existingUser, userPasswordHash, userPasswordSalt, user);
            await _userDal.UpdateAsync(updatedUser);
            return new SuccessResult("User updated successfully.");
        }
    }
}
