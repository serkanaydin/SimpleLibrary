#nullable enable
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleLibrary.Core.Dtos.Authentication;
using SimpleLibrary.Core.Enum;
using SimpleLibrary.Core.Helper.Authentication;
using SimpleLibrary.Core.Helper.ResponseHelper;
using SimpleLibrary.Domain;

namespace SimpleLibrary.Persistence.Repository
{
    public class AccountRepository : BaseRepository,IRepository {
        
        private UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IResponseHelper _responseHelper;

        public AccountRepository(SignInManager<User> signInManager,UserManager<User> userManager,
            DbContext context,IConfiguration configuration,IResponseHelper responseHelper) : base(context)
        {
            _responseHelper = responseHelper;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public async Task<ServiceResponse> RegisterUser(RegisterDto model)
            {
                var userExists = await _userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                    return _responseHelper.SetError("Username already exists",isLogging:true);
                
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
                    return _responseHelper.SetError("Unsuccessful registration");

                return _responseHelper.SetSuccess();
            }

        public async Task<DeactivateResult> DeactivateUser(int userId)
        {
            var userSet = Set<User>();
            var user = await  userSet.FirstOrDefaultAsync(q => q.Id == userId && q.IsActive);
            if (user is null)
                return DeactivateResult.ActiveUserDoesntExistWithUserId;

            user.IsActive = false;
            user.IsDeleted = true;
            user.UpdateDate=DateTime.Now;

            userSet.Update(user);
            var result = await SaveChangesAsync();
            if (result is 0)
                return DeactivateResult.SaveChangesFault;

            return DeactivateResult.DeactivateSuccessful;
        }
        
        public async Task<LoginResponseDto> Login(LoginDto login)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(q => q.UserName == login.Username);
            if (user is null)
                return new LoginResponseDto(){Result = LoginResult.NoUserWithTheUsername};
            
            var result =await _signInManager.CheckPasswordSignInAsync(user, login.Password, true);
            
            switch (result.Succeeded)
            {
                case true:
                    return JwtHelper.generateJwtToken(user,_configuration);; ;
                case false when result.IsLockedOut:
                    return new LoginResponseDto() { Result = LoginResult.LockedOut };
                case false when result.IsNotAllowed:
                    return new LoginResponseDto() { Result = LoginResult.NotAllowed };
                default:
                    return new LoginResponseDto() { Result = LoginResult.WrongPassword };
            }
        } 
    }
}