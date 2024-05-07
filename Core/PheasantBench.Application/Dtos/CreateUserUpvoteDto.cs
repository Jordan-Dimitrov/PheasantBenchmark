using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PheasantBench.Application.Dtos
{
    public class CreateUserUpvoteDto
    {
        [Range(-1,1)]
        public byte Score {  get; set; }
    }
}
