using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyMonitoring.Data.Contracts;
using CurrencyMonitoring.Data.EF;
using CurrencyMonitoring.Data.Models;

namespace CurrencyMonitoring.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CurrencyDbContext _dbContext;
        public UserRepository(CurrencyDbContext currencyDbContext)
        {
            _dbContext = currencyDbContext;
        }

        public UserRepository() : this(new CurrencyDbContext())
        {

        }

        public void AddNewUser(string login, string password)
        {
            login = login.ToUpper();
            if(string.IsNullOrEmpty(login) || password.Length <= 3)
            {
                return;
            }

            try
            {
                _dbContext.Users.Add(new User() { Login = login, Password = password });
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool IsLoginFree(string login)
        {
            try
            {
                return _dbContext.Users.Any(u => u.Login == login.ToUpper());
            }
            catch
            {
                return true;
            }
        }

        public bool IsUserExist(string login, string password)
        {
            return _dbContext.Users.Any(x=> x.Login == login.ToUpper() && x.Password == password);
        }
    }
}
