using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace FeelTags.WebApi.Startup
{
    public static class ConfigurationExtensions
    {
        public static void InitFirebaseAuth(this IConfiguration configuration)
        {
            string? serviceAccountKeyPath = configuration["FirebaseConfig:ServiceAccountKeyPath"];
            ArgumentException.ThrowIfNullOrWhiteSpace(serviceAccountKeyPath);
            if (!File.Exists(serviceAccountKeyPath))
            {
                throw new Exception($"File {serviceAccountKeyPath} doesn't exists");
            }
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(serviceAccountKeyPath),
            });
        }
    }
}
