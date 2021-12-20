using System.ComponentModel.DataAnnotations;

namespace SimpleLibrary.Core.Dtos
{
    public record BookTypeModelDto
    {
        [Required]
        public string Type { get; set; }
    }
}