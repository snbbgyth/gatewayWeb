using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abot.Poco;
using GateWay.DbHelper.Common;
using GateWay.DbHelper.Model;

namespace GateWay.Core.DAL
{
    public class HandlerFanyiyuedu:HandlerBase
    {
        public static HandlerFanyiyuedu Current
        {
            get { return new HandlerFanyiyuedu();}
        }

        public override bool SaveContent(CrawledPage crawledPage)
        {
            try
            {
                if (crawledPage.Uri.AbsoluteUri.Contains("http://www.hjenglish.com/new/p"))
                {
                    var titleNode = crawledPage.HtmlDocument.DocumentNode.SelectNodes("//div")
                        .SingleOrDefault(t => t.Attributes.Any(s => s.Name == "class" && s.Value == "page_title"));
                    if(titleNode==null)
                        return true;
                    var contentNode= crawledPage.HtmlDocument.DocumentNode.SelectNodes("//div")
                        .SingleOrDefault(t => t.Attributes.Any(s => s.Name == "class" && s.Value == "main_article"));
                    if (contentNode == null)
                        return true;

                    var fromNode = crawledPage.HtmlDocument.DocumentNode.SelectNodes("//div")
                      .SingleOrDefault(t => t.Attributes.Any(s => s.Name == "class" && s.Value == "page_tip"));

                   var author = fromNode == null ? "" : fromNode.InnerText.Split('|')[0];
                   var from = fromNode == null ? "" : fromNode.InnerText.Split('|').Length>1 ? fromNode.InnerText.Split('|')[1]:"";
                    var entity=new EnglishReadArticle();
                    entity.FromUrl = crawledPage.Uri.AbsoluteUri;
                    entity.Id=Guid.NewGuid();
                    entity.CreateDateTime=DateTime.Now;
                    entity.Title = titleNode.InnerText.Trim('\r','\n').Trim();
                    entity.Content = contentNode.InnerText;
                    entity.Author =string.IsNullOrEmpty(author)?"":author.Trim('\r', '\n').Trim().Replace("作者：","");
                    entity.From = string.IsNullOrEmpty(from) ? "" : from.Trim('\r', '\n').Trim().Replace("来源：", "");

                }
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError(ex,GetType(),MethodBase.GetCurrentMethod().Name);
                return false;
            }
            return true;
        }
    }
}
