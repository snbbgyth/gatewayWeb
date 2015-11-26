using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GateWay.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
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
    }
}