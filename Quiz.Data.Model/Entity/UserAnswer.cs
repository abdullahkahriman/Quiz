using Quiz.Core;
using Quiz.Data.Model.System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz.Data.Model.Entity
{
    public class UserAnswer : Superior
    {
        public long UserID { get; set; }
        public long AnswerID { get; set; }

        [ForeignKey(nameof(UserID))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(AnswerID))]
        public virtual Answer Answer { get; set; }
    }
}
