using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GateWay.DbHelper.Model;

namespace GateWay.DbHelper.Mapping
{
   public class EnglishReadArticleMapping:EntityTypeConfiguration<EnglishReadArticle>
    {
       public EnglishReadArticleMapping()
       {
           ToTable("EnglishReadArticle");
           HasKey(t => t.Id);
           Property(t => t.PublishDateTime);
           Property(t => t.ArticleTypeId);
           Property(t => t.Author);
           Property(t => t.Content);
           Property(t => t.CreateDateTime);
           Property(t => t.From);
           Property(t => t.FromUrl);
           Property(t => t.IsDelete);
           Property(t => t.Summary);
           Property(t => t.Title);
       }
    }
}
