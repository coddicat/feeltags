using System.ComponentModel.DataAnnotations;

namespace FeelTags.WebApi.Dal.Entities
{    
    public class Account
    {
        [Key]
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Picture { get; set; }
        public required AuthProvider AuthProvider { get; set; }
        public string? FirebaseUid { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum AuthProvider
    {
        Firebase
    }
}
