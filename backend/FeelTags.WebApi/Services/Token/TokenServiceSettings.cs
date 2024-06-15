using System.ComponentModel.DataAnnotations;

namespace FeelTags.WebApi.Services.Token
{
    public class TokenServiceSettings
    {
        public const string SECTION = "Token";

        [Required]
        public required string Secret { get; set; }

        [Required]
        public required string Issuer { get; set; }

        [Required]
        public required string Audience { get; set; }
    }
}
