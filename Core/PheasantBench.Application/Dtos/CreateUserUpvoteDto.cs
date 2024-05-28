using System.ComponentModel.DataAnnotations;

namespace PheasantBench.Application.Dtos
{
    public class CreateUserUpvoteDto
    {
        [Range(-1, 1)]
        public byte Score { get; set; }
    }
}
