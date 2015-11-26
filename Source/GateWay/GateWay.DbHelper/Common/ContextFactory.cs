using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GateWay.DbHelper.BLL;

namespace GateWay.DbHelper.Common
{
    public static class ContextFactory
    {

        //just for query
        public static DataContextBll _dataContext = new DataContextBll();

        public static DataContextBll DataSource
        {
            get
            {

                if (_dataContext == null)
                {
                    _dataContext = new DataContextBll();
                    _dataContext.Database.CommandTimeout = int.MaxValue;
                }
                return _dataContext;
            }
        }

    }
}
