﻿#nullable enable
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SimpleLibrary.Abstractions;
using SimpleLibrary.Core.Dtos.Authentication;
using SimpleLibrary.Core.Enum;
using SimpleLibrary.Domain;
using SimpleLibrary.Persistence.Repository;

namespace SimpleLibrary.Application
{
    public class AccountService : IApplicationService
    {
        private AccountRepository _accountRepository;
        public AccountService(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool?> RegisterUser(RegisterDto model)
        {
           return await _accountRepository.RegisterUser(model);
        }

        public async Task<LoginResponseDto> Login(LoginDto login)
        {
            return await _accountRepository.Login(login);
        }
        public async Task<UserEnums> DeactivateUser(int userId)
        {
            return await _accountRepository.DeactivateUser(userId);
        }
    }
}