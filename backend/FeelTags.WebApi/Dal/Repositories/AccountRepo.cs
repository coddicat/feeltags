using FeelTags.WebApi.Dal.DTOs;
using FeelTags.WebApi.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace FeelTags.WebApi.Dal.Repositories
{
    public interface IAccountRepo
    {
        Task<long> CreateOrUpdateFirebaseAccountAsync(string uid, AccountDTO dto);
        Task<Account?> GetFirebaseAccountAsync(string uid);
    }
    public class AccountRepo : IAccountRepo
    {
        private readonly IFeelTagsContext _context;

        public AccountRepo(IFeelTagsContext context)
        {
            _context = context;
        }

        public Task<Account?> GetFirebaseAccountAsync(string uid)
        {
            return _context.Accounts.FirstOrDefaultAsync(x => x.FirebaseUid == uid);
        }

        public async Task<long> CreateOrUpdateFirebaseAccountAsync(string uid, AccountDTO dto)
        {
            Account? account = await _context.Accounts.FirstOrDefaultAsync(x => x.FirebaseUid == uid);
            if (account is null)
            {
                account = new Account
                {
                    Name = string.Empty,
                    Email = string.Empty,
                    Picture = string.Empty,
                    AuthProvider = AuthProvider.Firebase,
                    FirebaseUid = uid
                };
                await _context.Accounts.AddAsync(account);
            } 

            dto.MapToDbAccount(account);

            await _context.SaveChangesAsync();

            return account.Id;
        }
    }
}
