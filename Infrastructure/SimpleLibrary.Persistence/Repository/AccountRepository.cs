#nullable enable
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SimpleLibrary.Core.Dtos.Authentication;
using SimpleLibrary.Core.Enum;
using SimpleLibrary.Core.Helper.Authentication;
using SimpleLibrary.Domain;

namespace SimpleLibrary.Persistence.Repository
{
    public class AccountRepository : BaseRepository
    {
        
        private UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        
        public AccountRepository(SignInManager<User> signInManager,UserManager<User> userManager, DbContext context,IConfiguration configuration) : base(context)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public async Task<bool?> RegisterUser(RegisterDto model)
            {
                var userExists = await _userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                    return null;
                
                User user = new User()  
                {  
                    Email = model.Email,  
                    SecurityStamp = Guid.NewGuid().ToString(),  
                    UserName = model.Username,
                    IsActive = true,
                    IsDeleted = false
                };  
                
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return null;

                return true;
            }

        public async Task<UserEnums> DeactivateUser(int userId)
        {
            var userSet = Set<User>();
            var user = await  userSet.FirstOrDefaultAsync(q => q.Id == userId && q.IsActive);
            if (user is null)
                return UserEnums.ActiveUserDoesntExistWithUserId;

            user.IsActive = false;
            user.IsDeleted = true;
            user.UpdateDate=DateTime.Now;

            userSet.Update(user);
            var result = await SaveChangesAsync();
            if (result is 0)
                return UserEnums.SaveChangesFault;

            return UserEnums.DeactivateSuccessful;
        }
        
        public async Task<LoginResponseDto> Login(LoginDto login)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(q => q.UserName == login.Username);
            if (user is null)
                return new LoginResponseDto(){result = UserEnums.NoUserWithTheUsername};
            
            var result =await _signInManager.CheckPasswordSignInAsync(user, login.Password, true);
            
            switch (result.Succeeded)
            {
                case true:
                    return JwtHelper.generateJwtToken(user,_configuration);; ;
                case false when result.IsLockedOut:
                    return new LoginResponseDto() { result = UserEnums.LockedOut };
                case false when result.IsNotAllowed:
                    return new LoginResponseDto() { result = UserEnums.NotAllowed };
                default:
                    return new LoginResponseDto() { result = UserEnums.WrongPassword };
            }
        } 
    }
}