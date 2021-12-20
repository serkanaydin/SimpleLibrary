using System.Collections.Generic;

namespace SimpleLibrary.Core.Dtos
{
    public record SearchByBookTypeResultDto
    {
        public int TotalCount { get; set; }
        public string Type { get; set; }
        public int CurrentPage { get; set; }
        public IList<BookInfoDto> BookInfoList { get; set; }
    }
}