using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GateWay.DbHelper.Model;

namespace GateWay.DbHelper.Mapping
{
    public  class ArticleTypeMapping:EntityTypeConfiguration<ArticleType>
    {
        public ArticleTypeMapping()
        {
            ToTable("ArticleType");
            HasKey(t => t.Id);
            Property(t => t.IsDelete);
            Property(t => t.CreateDateTime);
            Property(t => t.Name);
            Property(t => t.Type);
        }
    }
}
