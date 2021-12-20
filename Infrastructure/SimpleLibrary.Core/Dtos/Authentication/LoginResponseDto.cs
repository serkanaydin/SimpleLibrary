using System;
using System.ComponentModel.DataAnnotations;
using SimpleLibrary.Core.Enum;

namespace SimpleLibrary.Core.Dtos.Authentication
{
    public record LoginResponseDto
    {
        [Required]
        public UserEnums result { get; set; }
        public string JwtToken { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}