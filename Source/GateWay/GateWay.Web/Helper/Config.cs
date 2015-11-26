using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GateWay.Web.Helper
{
    public static class Config
    {

        public static Action<Guid> SaveUserId = delegate (Guid token)
        {
            if (HttpContext.Current.Session != null)
                HttpContext.Current.Session["AccountId"] = token;
        };

        public static Func<Guid> GetUserId = delegate()
        {
            try
            {
                var session = HttpContext.Current.Session;
                if (session != null && session["AccountId"] != null)
                    return new Guid(session["AccountId"].ToString()) ;

                var controller = HttpContext.Current;
                var token = controller.Request.Headers["AccountId"] ?? controller.Request.QueryString["AccountId"] ?? controller.Request.Form["AccountId"];
                return new Guid(token);
            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
        };
    }
}