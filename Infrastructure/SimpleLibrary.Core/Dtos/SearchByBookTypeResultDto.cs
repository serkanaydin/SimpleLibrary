using System.Collections.Generic;

namespace SimpleLibrary.Core.Dtos
{
    public class SearchByBookTypeResultDto
    {
        public int totalCount { get; set; }
        public IList<BookInfoDto> bookInfoList { get; set; }
    }
}