using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;
using PheasantBench.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PheasantBench.Application.Abstractions
{
    public interface IUserUpvoteService
    {
        Task<Response> UpvoteAsync(Guid forumMessageId, string userId, CreateUserUpvoteDto value);
    }
}
