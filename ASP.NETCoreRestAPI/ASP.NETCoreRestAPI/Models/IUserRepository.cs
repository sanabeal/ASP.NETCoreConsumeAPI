using ASP.NETCoreRestAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreRestAPI.Models
{
    public interface IUserRepository
    {

        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int userId);
        Task<User> GetUserByEmail(string userID);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task DeleteUser(int userId);
    }
}
