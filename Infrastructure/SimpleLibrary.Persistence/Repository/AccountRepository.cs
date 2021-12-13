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
using SimpleLibrary.Domain;

namespace SimpleLibrary.Persistence.Repository
{
    public class AccountRepository : BaseRepository
    {
        private UserManager<User> _userManager;
        private IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;

        public AccountRepository(SignInManager<User> signInManager,UserManager<User> userManager, DbContext context,IConfiguration configuration) : base(context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<string?> RegisterUser(RegisterDto model)
            {
                var userExists = await _userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                    return null;
                User user = new User()  
                {  
                    Email = model.Email,  
                    SecurityStamp = Guid.NewGuid().ToString(),  
                    UserName = model.Username  
                };  
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return null;

                return generateJwtToken(user);
            }  
        public async Task<string?> Login(LoginDto login)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(q => q.UserName == login.Username);
            if (user is null)
                return null;
            var result =await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (result.Succeeded is false)
                return null;
            return generateJwtToken(user);;
        } 
    }
}