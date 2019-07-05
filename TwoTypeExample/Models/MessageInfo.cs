using System;
using SQLite;
namespace TwoTypeExample.Models
{
    [Table("MessageInfo")]
    public class MessageInfo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime MessageCreated { get; set; }
        public int FromContactId { get; set; }
        public string Message { get; set; }
    }
}
