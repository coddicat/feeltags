namespace FeelTags.WebApi.Dal.DTOs
{
    public class ResponseDTO
    {
        public required long AnswerOptionId { get; set; }
        public Location? Location { get; set; }
    }
    public record Location(double Latitude, double Longitude);
}
