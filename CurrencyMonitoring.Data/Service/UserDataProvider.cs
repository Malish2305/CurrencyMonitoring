using CurrencyMonitoring.Data.Contracts;
using CurrencyMonitoring.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CurrencyMonitoring.Data.Service
{
    public class UserDataProvider
    {
        IUserRepository _userRepository;

        public UserDataProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDataProvider() : this(new UserRepository())
        {

        }

        public void AddNewUser(string login, string password)
        {
            login = login.ToUpper();
            _userRepository.AddNewUser(login, password);

        }
        public bool IsLoginFree(string login)
        {
            login = login.ToUpper();
            return _userRepository.IsLoginFree(login);
        }
        public bool IsUserExist(string login, string password)
        {
            login = login.ToUpper();
            return _userRepository.IsUserExist(login, password);
        }

    }
}
