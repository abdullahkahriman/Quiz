using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz.Data.Model.Entity
{
    public class QuestionAnswer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public long QuestionID { get; set; }
        public long AnswerID { get; set; }

        [ForeignKey(nameof(QuestionID))]
        public virtual Question Question { get; set; }

        [ForeignKey(nameof(AnswerID))]
        public virtual Answer Answer { get; set; }
    }
}