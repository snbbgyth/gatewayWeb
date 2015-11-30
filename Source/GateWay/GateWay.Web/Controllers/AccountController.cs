using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GateWay.DbHelper.Common;
using GateWay.Web.DAL;
using GateWay.Web.Helper;
using GateWay.Web.Models;

namespace GateWay.Web.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var entity=new LoginViewModel();
            entity.Email = Request.Cookies["WoXue"] == null ? "" :  Request.Cookies["WoXue"]["UserName"];
            entity.Password = Request.Cookies["WoXue"] == null ? "" : Request.Cookies["WoXue"]["Password"] ;
            return View(entity);
        }

        private void ClearCookie()
        {
            var cookie = Request.Cookies["WoXue"];
            if (cookie != null)
            {
                cookie.Values.Remove("UserName");
                cookie.Values.Remove("Password");
                TimeSpan ts = new TimeSpan(-1, 0, 0, 0);
                cookie.Expires = DateTime.Now.Add(ts);//删除整个Cookie，只要把过期时间设置为现在
                Response.AppendCookie(cookie);
            }
        }

        private void SetCookie(string userName,string password)
        {
            var cookie = Request.Cookies["WoXue"];
            if (cookie == null)
            {
                cookie=new HttpCookie("WoXue");
            }
            if (!string.IsNullOrEmpty(userName))
                cookie.Values.Set("UserName", userName);
            if (!string.IsNullOrEmpty(password))
                cookie.Values.Set("Password", password);
            TimeSpan ts = new TimeSpan(7, 0, 0, 0, 0);//过期时间为1分钟
            cookie.Expires = DateTime.Now.Add(ts);//删除整个Cookie，只要把过期时间设置为现在
            Response.AppendCookie(cookie);
        }

        public ActionResult LoginOut()
        {
            Config.SaveUserId(Guid.Empty);
            ClearCookie();
            return View("Login");
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid || !model.Code.Equals(Session["VerifyCode"].ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    if (!model.Code.Equals(Session["VerifyCode"].ToString(), StringComparison.OrdinalIgnoreCase))
                        ViewBag.Message = "验证码错误";
                    return View(model);
                }
                // 这不会计入到为执行帐户锁定而统计的登录失败次数中
                // 若要在多次输入错误密码的情况下触发帐户锁定，请更改为 shouldLockout: true
                var result = await CheckLogin(model);
                if (model.RememberMe)
                {
                    SetCookie(model.Email,model.Password);
                }
                if (result) return RedirectToLocal(returnUrl);
                ViewBag.Message = "用户名或者密码错误，请重新输入";
                //Response.Write("<script>alert('用户名或者密码错误，请重新输入')</script>");
                return View(model);
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError(ex, GetType(), MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public async Task<bool> CheckLogin(LoginViewModel model)
        {
            var account =await DataSource.Accounts.FirstOrDefaultAsync( t => t.Name == model.Email || t.Email == model.Email||t.Phone==model.Email);
            if (account == null) return false;
            var password = UntilHelper.GetMd5HashCode(model.Password + account.Salt);
            if (password == account.Password)
            {
                Config.SaveUserId(account.Id);
                return true;
            }
            return false;
        }

        public ActionResult LogOff()
        {
            return View("Login");
        }

        public ActionResult ConfirmEmail()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        public ActionResult SendCode()
        {
            return View();
        }

        public ActionResult VerifyCode()
        {
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}