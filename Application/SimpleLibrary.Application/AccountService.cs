#nullable enable
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SimpleLibrary.Core.Dtos.Authentication;
using SimpleLibrary.Persistence.Repository;

namespace SimpleLibrary.Application
{
    public class AccountService
    {
        private AccountRepository _accountRepository;
        public AccountService(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<string?> RegisterUser(RegisterDto model)
        {
           return await _accountRepository.RegisterUser(model);
        }

        public async Task<string?> Login(LoginDto login)
        {
            return await _accountRepository.Login(login);
        }
    }
}