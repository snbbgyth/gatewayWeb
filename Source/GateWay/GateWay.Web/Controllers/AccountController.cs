using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GateWay.DbHelper.Common;
using GateWay.DbHelper.Model;
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
            var entity = new LoginViewModel();
            entity.Email = Request.Cookies["WoXue"] == null ? "" : Request.Cookies["WoXue"]["UserName"];
            entity.Password = Request.Cookies["WoXue"] == null ? "" : Request.Cookies["WoXue"]["Password"];
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

        private void SetCookie(string userName, string password)
        {
            var cookie = Request.Cookies["WoXue"];
            if (cookie == null)
            {
                cookie = new HttpCookie("WoXue");
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
                if (!ModelState.IsValid || model.Code==null|| !model.Code.Equals(Session["VerifyCode"].ToString(), StringComparison.OrdinalIgnoreCase))
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
                    SetCookie(model.Email, model.Password);
                }
                if (result)
                {
                    FormsAuthentication.SetAuthCookie(Account.Name, true);
                    return RedirectToLocal(returnUrl);
                }
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
            var account = await DataSource.Accounts.FirstOrDefaultAsync(t => t.Name == model.Email || t.Email == model.Email || t.Phone == model.Email);
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
            ClearCookie();
            Config.SaveUserId(Guid.Empty);
            return View("Login");
        }

        public ActionResult ConfirmEmail()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            var model = new ForgotPasswordViewModel() { Message = String.Empty };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            try
            {

                model.Message = String.Empty;
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = DataSource.Accounts.FirstOrDefault(t => t.Email == model.Email && t.Name == model.Email);
                if (user == null)
                {
                    model.Message = "错误的用户名或者邮箱";
                    return View(model);
                }

                Response.Write("<script>alert('邮件已发送，请立即查收!')</script>");
                return View("Login");
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError(ex, GetType(), MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            var model = new RegisterViewModel { Message = string.Empty };
            return View(model);
        }

        public ActionResult GetValidateCode(string time)
        {
            try
            {
                string code = UntilHelper.CreateRandomCode(4);
                Session["VerifyCode"] = code;
                byte[] bytes = UntilHelper.CreateImage(code);
                return File(bytes, @"image/jpeg");
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError(ex, GetType(), MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                string message = string.Empty;
                if (!ModelState.IsValid || CheckRegister(model, out message))
                {
                    model.Message = message;
                    return View(model);
                }
                // 如果我们进行到这一步时某个地方出错，则重新显示表单
                var result = await AddAccount(model);
                if (result) return View("Login");
                model.Message = "注册失败";
                return View(model);
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError(ex, GetType(), MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        private bool CheckRegister(RegisterViewModel model, out string message)
        {
            message = string.Empty;
            var isEmail = DataSource.Accounts.Any(t => t.Email == model.Email);
            if (isEmail)
                message = "此邮箱己被注册";
            var isName = DataSource.Accounts.Any(t => t.Name == model.AccountName);
            if (isName)
                message = "此帐户名己被注册";
            var isPhone = DataSource.Accounts.Any(t => t.Phone == model.Phone);
            if (isPhone)
                message = "此手机号己被注册";
            return isEmail && isName && isPhone;
        }

        private async Task<bool> AddAccount(RegisterViewModel model)
        {
            var entity = new Account();
            entity.Id = Guid.NewGuid();
            entity.Email = model.Email;
            entity.Name = model.AccountName;
            entity.Phone = model.Phone;
            entity.CreateDateTime = DateTime.Now;
            entity.Salt = Guid.NewGuid().ToString().Replace("-", "");
            entity.Password = UntilHelper.GetMd5HashCode(model.Password + entity.Salt);
            DataSource.Accounts.Add(entity);
            await DataSource.SaveChangesAsync();
            return true;
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