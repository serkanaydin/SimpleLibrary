using System.ComponentModel.DataAnnotations;

namespace SimpleLibrary.Core.Dtos.Authentication
{
    public record RegisterDto
    {
        [Required]
        public string Username { get; set; }  
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }  
  
        public string Password { get; set; }
    }
}