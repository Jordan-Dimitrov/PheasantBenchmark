﻿namespace PheasantBench.Application.ViewModels
{
    public class ForumMessageDto
    {
        public Guid Id { get; set; }
        public string MessageContent { get; set; }
        public string? FileName { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid ForumThreadId { get; set; }
        public UserDto User { get; set; }
        public long UpvoteCount { get; set; }
    }
}
