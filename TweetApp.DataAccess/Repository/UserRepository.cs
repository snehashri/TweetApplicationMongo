using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetApp.Domain.DBContext;
using TweetApp.Domain.Models;

namespace TweetApp.Domain.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> AddUser(User user);
        Task<User> GetUserById(int id);
        Task<bool> IsRegisteredUser(string useremail);
        Task<bool> ResetPassword(ResetPasswordModel model);
        Task<bool> ForgetPassword(ForgetPasswordModel model);      
        Task<bool> UpdateUserStatusLogIn(int id);
        Task<bool> UpdateUserStatusLogOut(int id);
    }
    public class UserRepository : IUserRepository
    {
        private readonly TweetAppDatabaseSettings _context;
        public UserRepository(TweetAppDatabaseSettings context)
        {
            _context = context;
        }
        public async Task<bool> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            int Ischanged = await _context.SaveChangesAsync();
            return Ischanged > 0;

        }

        

        public async Task<bool> ForgetPassword(ForgetPasswordModel model)
        {
            int Ischanged=0;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if(user.DOB.ToString().Split(" ")[0]==model.DOB.ToString().Split(" ")[0])
            {
                user.Password = model.NewPassword;                
                Ischanged = await _context.SaveChangesAsync();
            }  
            return Ischanged > 0;

        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email); 
            return user;
        }
        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }

        public async Task<bool> IsRegisteredUser(string useremail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == useremail); 
            if (user!=null)
            {
                return true;
            }
            return false;
        }

        

        public async Task<bool> ResetPassword(ResetPasswordModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if(model.OldPassword==user.Password)
            {
                user.Password = model.NewPassword;
            }
            int Ischanged = await _context.SaveChangesAsync();
            return Ischanged > 0;
        }

       

        public async Task<bool> UpdateUserStatusLogIn(int id)
        {
            var user = await _context.Users.FindAsync(id);
            user.IsActive = true;
            int Ischanged = await _context.SaveChangesAsync();
            return Ischanged > 0;
        }

        public async Task<bool> UpdateUserStatusLogOut(int id)
        {
            var user = await _context.Users.FindAsync(id);
            user.IsActive = false;
            int Ischanged = await _context.SaveChangesAsync();
            return Ischanged > 0;
        }
    }
}
