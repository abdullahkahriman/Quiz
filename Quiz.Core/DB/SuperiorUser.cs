using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Quiz.Core.DB
{
    public abstract class SuperiorUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public long CreatedUserID { get; set; }
        public long? UpdatedUserID { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }
}
