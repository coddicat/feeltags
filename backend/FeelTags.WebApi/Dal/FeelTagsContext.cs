using FeelTags.WebApi.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace FeelTags.WebApi.Dal
{
    public interface IFeelTagsContext
    {        
        DbSet<Question> Questions { get; set; }
        DbSet<Response> Responses { get; set; }
        DbSet<Account> Accounts { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
    public class FeelTagsContext(DbContextOptions<FeelTagsContext> options) : DbContext(options), IFeelTagsContext
    {
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Response> Responses { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDbFunction(() => SqlFunctions.BiasRandomByDate(default))
                .HasName(nameof(SqlFunctions.BiasRandomByDate));

            base.OnModelCreating(modelBuilder);
        }
    }
}
