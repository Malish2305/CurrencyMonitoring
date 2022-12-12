using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyMonitoring.Data.Contracts
{
    public interface IUserRepository
    {
        public void AddNewUser(string login, string password);
        public bool IsLoginFree(string login);
        public bool IsUserExist(string login, string password);
    }
}
