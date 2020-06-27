using Quiz.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz.Data.Model.System
{
    public class UserRole : Superior
    {
        public long UserID { get; set; }
        public long RoleID { get; set; }

        [ForeignKey(nameof(UserID))]
        public User User { get; set; }

        [ForeignKey(nameof(RoleID))]
        public Role Role { get; set; }
    }
}