using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeelTags.WebApi.Dal.Entities
{
    public class Response
    {
        [Key]
        public long Id { get; set; }
        public long AnswerOptionId { get; set; }
        public long AccountId { get; set; }

        [ForeignKey(nameof(AnswerOptionId))]
        public virtual AnswerOption? AnswerOption { get; set; }
        
        [ForeignKey(nameof(AccountId))]
        public virtual Account? Account { get; set; }
        
        public DateTime? ResponseTime { get; set; } = DateTime.UtcNow;

        public Point? Location { get; set; }        
    }
}
