using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeelTags.WebApi.Dal.Entities
{
    public class Question
    {
        [Key]
        public long Id { get; set; }
        public long AccountId { get; set; }

        public required string Content { get; set; }

        [InverseProperty("Question")]
        public virtual List<AnswerOption>? AnswerOptions { get; set; }

        [ForeignKey(nameof(AccountId))]
        public virtual Account? Account { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public QuestionStatus Status { get; set; } = QuestionStatus.Active;
    }
}
