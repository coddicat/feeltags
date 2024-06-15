using FeelTags.WebApi.Dal.Entities;

namespace FeelTags.WebApi.Dal.DTOs
{
    public class AccountDTO
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Picture { get; set; }

        public void MapToDbAccount(Account dbAccount)
        {
            dbAccount.Name = Name;
            dbAccount.Email = Email;
            dbAccount.Picture = Picture;
        }

        public static AccountDTO FromDbAccount(Account dbAccount)
        {
            return new AccountDTO
            {
                Email = dbAccount.Email,
                Name = dbAccount.Name,
                Picture = dbAccount.Picture
            };
        }
    }
}
