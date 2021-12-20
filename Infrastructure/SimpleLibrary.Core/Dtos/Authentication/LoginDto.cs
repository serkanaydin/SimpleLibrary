using System.ComponentModel.DataAnnotations;

namespace SimpleLibrary.Core.Dtos.Authentication
{
    public record LoginDto
    {
        [Required]
        public string Username { get; set; } 
        [Required]
        public string Password { get; set; }

    }
}