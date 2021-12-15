using System;

namespace SimpleLibrary.Domain
{
    public class User : Microsoft.AspNetCore.Identity.IdentityUser<int>
    {
        public string Name { get; set; }

        public string Surname { get; set; }
        
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}