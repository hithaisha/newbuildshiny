using MORR.Application.DTOs.UserDTOs;
using MORR.Domain.Entities;

namespace MORR.Application.Common.Extentions
{
    public static class UserExtention
    {
        public static User ToEntity(this UserDTO userDTO, User? user = null)
        {
            if(user is null) user = new User();

            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.Email = userDTO.Email;
            user.UserName = userDTO.Email;
            user.IsActive = true;

            if(userDTO.Id == 0)
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
            }
            
           

            return user;
        }
    }
}
