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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccountMapping());

        }
    }
}
