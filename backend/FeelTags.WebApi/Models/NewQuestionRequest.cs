namespace FeelTags.WebApi.Models
{
    public class NewQuestionRequest
    {
        public required string Content { get; set; }
        public required string[] AnswerOptions { get; set; }
    }
}
