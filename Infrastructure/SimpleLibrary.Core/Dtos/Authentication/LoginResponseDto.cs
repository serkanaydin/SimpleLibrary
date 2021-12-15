using System;

namespace SimpleLibrary.Core.Dtos.Authentication
{
    public record LoginResponseDto
    {
        public string JwtToken { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}