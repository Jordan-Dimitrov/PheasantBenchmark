using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PheasantBench.Application.ViewModels
{
    public class ForumThreadPagedDto
    {
        public IEnumerable<ForumThreadDto> ForumThreads { get; set; }
        public int TotalPages { get; set; }
    }
}
