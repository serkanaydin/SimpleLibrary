using System;

namespace SimpleLibrary.Core.Dtos.Authentication
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}