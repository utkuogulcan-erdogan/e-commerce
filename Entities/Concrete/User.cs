using Core.Entites;
using Core.Utilities.Results;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }   
        public byte[] PasswordSalt { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public Basket Basket { get; set; }
        public ICollection<Order> Orders { get; set; }

        public static User CreateUser(
            UserAddDto userDto,
            byte[] passwordHash,
            byte[] passwordSalt)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                UserName = userDto.UserName,
                Email = userDto.Email,
                FullName = userDto.FullName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static User UpdateUser(
            User user,
            byte[] passwordHash,
            byte[] passwordSalt,
            UserUpdateDto dto)
        {
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNull(dto);
            if (!string.IsNullOrWhiteSpace(dto.FullName))
            {
                user.FullName = dto.FullName;
            }
            if (!string.IsNullOrWhiteSpace(dto.UserName))
            {
                user.UserName = dto.UserName;
            }
            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                user.Email = dto.Email;
            }
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            return user;
        }
    }
}
