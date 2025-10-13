using Core.DataAccess;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        public Task<List<UserDisplayDto>> GetAllUserAsync();
        public Task<UserDisplayDto> GetUserAsync(string email);
        public Task AddUserAsync(UserAddDto user, byte[] passwordHash, byte[] passwordSalt);

    }
}
