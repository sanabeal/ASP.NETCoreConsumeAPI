using ASP.NETCoreRestAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreRestAPI.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext AppDbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            this.AppDbContext = appDbContext;
        }

        public async Task<User> AddUser(User user)
        {
            //if (user.UserID != null)
            //{
            //    AppDbContext.Entry(user.UserID).State = EntityState.Unchanged;
            //}

            var result = await AppDbContext.Users.AddAsync(user);
            await AppDbContext.SaveChangesAsync();
            return result.Entity;        
        }

        public async Task DeleteUser(int userId)
        {
            var result = await AppDbContext.Users
                .FirstOrDefaultAsync(e => e.ID == userId);

            if (result != null)
            {
                AppDbContext.Users.Remove(result);
                await AppDbContext.SaveChangesAsync();
            }
        }

        public async Task<User> GetUser(int userId)
        {
            return await AppDbContext.Users.FirstOrDefaultAsync(e => e.ID == userId);
        }

        public async Task<User> GetUserByEmail(string userID)
        {
            return await AppDbContext.Users
                 .FirstOrDefaultAsync(e => e.UserID == userID);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await AppDbContext.Users.ToListAsync();
        }

        public async Task<User> UpdateUser(User user)
        {
            var result = await AppDbContext.Users
                .FirstOrDefaultAsync(e => e.ID == user.ID);

            if (result != null)
            {
                result.UserID = user.UserID;
                result.FirstName = user.FirstName;
                result.LastName = user.LastName;
                result.Password = user.Password;
                result.Email = user.Email;
                result.Phone = user.Phone;
                result.Gender = user.Gender;
                result.Address = user.Address;
                result.Status = user.Status;
                await AppDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
