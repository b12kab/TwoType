using System;
namespace TwoTypeExample.Models
{
    public class MessageInfoWithContact
    {
        public int MessageId { get; set; }
        public DateTime MessageCreated { get; set; }
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string MessageText { get; set; }
    }
}
