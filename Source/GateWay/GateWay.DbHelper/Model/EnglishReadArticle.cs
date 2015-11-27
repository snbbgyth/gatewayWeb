using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GateWay.DbHelper.Model
{
   public class EnglishReadArticle
    {
        public Guid Id { get; set; }

        public DateTime CreateDateTime { get; set; }

        public bool IsDelete { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string From { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public DateTime? PublishDateTime { get; set; }

        public string FromUrl { get; set; }

        public Guid? ArticleTypeId { get; set; }


    }
}
