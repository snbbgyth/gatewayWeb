using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GateWay.DbHelper.BLL;
using GateWay.DbHelper.Common;
using GateWay.DbHelper.Model;

namespace GateWay.Core.DAL
{
    public class DataQueueDal : BaseQueueDAL<dynamic>
    {
        private static object synObj = new object();

        private static DataQueueDal _instance;

        public static DataQueueDal Instance
        {
            get
            {
                lock (synObj)
                {
                    if (_instance == null)
                    {
                        _instance = new DataQueueDal();
                    }
                }
                return _instance;
            }
        }

        private DataQueueDal()
        {

        }

        protected override void OnNotify(dynamic entity)
        {
            if(entity is EnglishReadArticle)
                AddEnglishReadArticle(entity);
        }

        private void AddEnglishReadArticle(EnglishReadArticle entity)
        {
            if (string.IsNullOrEmpty(entity?.FromUrl)) return;
            using (var db = new DataContextBll())
            {
                var exist = db.EnglishReadArticles.FirstOrDefault(t => t.FromUrl == entity.FromUrl);
                if (exist == null)
                    db.EnglishReadArticles.Add(entity);
                else
                {
                    exist.Title = entity.Title;
                    exist.Content = entity.Content;
                    exist.From = entity.From;
                    exist.Author = entity.Author;
                    exist.Summary = entity.Summary;
                }
                db.SaveChanges();
            }
        }


    }
}
