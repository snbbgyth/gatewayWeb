using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GateWay.DbHelper.Model
{
   public class ArticleType
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateDateTime { get; set; }

        public bool IsDelete { get; set; }

        public int Type { get; set; }
    }
}
