using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using TPC_API.Contexts;
using TPC_API.Models;
using TPC_API.Utils;

namespace TPC_API.Services
{
    public class UserService
    {
        private readonly DataBaseContext _context;

        public UserService(DataBaseContext context)
        {
            _context = context;
        }

        public Result<User> Create(User user)
        {
            try
            {
                if (IsValidEmail(user.Email)) return Result.Fail<User>("Email já cadastrado");
                user = _context.Users.Add(user).Entity;
                _context.SaveChanges();
            }
            catch (Exception ex) 
            {
                return Result.Fail<User>(ex.Message);
            }
            return Result.Ok(user);
            
        }

        public Result<User> GetById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return Result.Fail<User>("Usuário não existe");
            return Result.Ok(user);
        }

        public IQueryable<User> GetAll() => _context.Users;

        public Result<User>  Update(User user)
        {
            var user_context = _context.Users.Find(user.Id);
            try
            {
                if (user_context == null) return Result.Fail<User>("Usuário não encontrado");
                user_context.Nome = user.Nome;
                user_context.Email = user.Email;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result.Fail<User>(ex.Message);
            }
           
            return Result.Ok(user_context);
        }

        public Result RemuveById(int id) 
        {
            try
            {
                var user_context = _context.Users.Find(id);
                if (user_context == null) return Result.Fail<User>("Usuário não encontrado");
                _context.Users.Remove(user_context);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result.Fail<User>(ex.Message);
            }
            
            return Result.Ok();
        }

        public bool IsValidEmail(String email) => _context.Users.Any(x=> x.Email.ToLower()==email.ToLower());

    }
}
