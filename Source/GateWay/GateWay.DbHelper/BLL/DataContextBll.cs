using System.Data.Entity;
using GateWay.DbHelper.Mapping;
using GateWay.DbHelper.Model;

namespace GateWay.DbHelper.BLL
{
    public class DataContextBll:DbContext
    {
        public DataContextBll()
          : base("DefaultConnection")
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<LogMessage> LogMessages { get; set; }
        public DbSet<EnglishReadArticle> EnglishReadArticles { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccountMapping());
            modelBuilder.Configurations.Add(new LogMessageMapping());
            modelBuilder.Configurations.Add(new EnglishReadArticleMapping());
            modelBuilder.Configurations.Add(new ArticleTypeMapping());
        }
    }
}
