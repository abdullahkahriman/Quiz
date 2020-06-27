using Quiz.Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Quiz.Data.Model.System
{
    public class User : Superior
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public long RoleID { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(RoleID))]
        public Role Role { get; set; }
    }
}