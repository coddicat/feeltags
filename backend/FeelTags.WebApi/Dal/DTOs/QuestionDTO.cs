namespace FeelTags.WebApi.Dal.DTOs
{
    public class QuestionDTO
    {
        public required string AuthorName { get; set; }
        public required string AuthorPicture { get; set; }        
        public required string Content { get; set; }
        public required Dictionary<long, string> AnswerOptions { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}
