using System.Collections.Generic;

namespace SimpleLibrary.Core.Dtos
{
    public record SearchByBookTypeResultDto
    {
        public int totalCount { get; set; }
        public IList<BookInfoDto> bookInfoList { get; set; }
    }
}