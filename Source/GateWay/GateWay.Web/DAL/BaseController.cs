using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GateWay.DbHelper.BLL;
using GateWay.DbHelper.Model;
using GateWay.Web.Helper;

namespace GateWay.Web.DAL
{
    public class BaseController : Controller
    {
        private DataContextBll _dataSource;

        public DataContextBll DataSource
        {
            get
            {
                if (_dataSource == null)
                    _dataSource = new DataContextBll();
                return _dataSource;
            }
        }

        private Guid _accountId;

        public Guid AccountId
        {
            get
            {
                if (_accountId == Guid.Empty)
                {
                    _accountId = Config.GetUserId();
                }
                return _accountId;
            }
        }

        private Account _account;

        public Account Account
        {
            get
            {
                if (_account == null&&AccountId!=Guid.Empty)
                    _account = DataSource.Accounts.FirstOrDefault(t => t.Id == AccountId);

                return _account;
            }
        }
    }
}