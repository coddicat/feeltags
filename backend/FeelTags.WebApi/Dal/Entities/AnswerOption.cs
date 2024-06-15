using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeelTags.WebApi.Dal.Entities
{
    public class AnswerOption
    {
        [Key]
        public long Id { get; set; }
        public long QuestionId { get; set; }
        
        public required string Text { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public virtual Question? Question { get; set; }
    }
}
