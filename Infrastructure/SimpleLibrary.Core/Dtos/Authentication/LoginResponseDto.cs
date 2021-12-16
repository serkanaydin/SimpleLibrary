using System;
using SimpleLibrary.Core.Enum;

namespace SimpleLibrary.Core.Dtos.Authentication
{
    public record LoginResponseDto
    {
        public UserEnums result { get; set; }
        public string JwtToken { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}