using MauiApi.Domain.Base;

namespace MauiApi.Domain.Entities.Messages
{
    public class MessageInfo : EntityBase
    {
        public string AccountId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}
