using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GateWay.Core.DAL;
using GateWay.DbHelper.Common;

namespace GateWay.Core.BLL
{
    public class HandleBll
    {
        public static HandleBll Current
        {
            get
            {
                return new HandleBll();
            }
        }


        public void StartCrawler()
        {
            try
            {
                HandlerFanyiyuedu.Current.Crawler("http://www.hjenglish.com/");
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError(ex, GetType(), MethodBase.GetCurrentMethod().Name);
            }
        }

    }
}
