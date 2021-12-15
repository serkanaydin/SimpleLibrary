namespace SimpleLibrary.Core.Dtos.Authentication
{
    public record LoginDto
    {
        public string Username { get; set; }  
        public string Password { get; set; }

    }
}