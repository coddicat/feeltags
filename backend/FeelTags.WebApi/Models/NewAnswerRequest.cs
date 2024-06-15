namespace FeelTags.WebApi.Models
{

    public class NewAnswerRequest
    {
        public required long AnswerOptionId { get; set; }
        public Location? Location { get; set; }
    }

    public record Location(double Latitude, double Longitude);
}
