﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebFinal2.Models;

namespace WebFinal2.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Login()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            using (DBContext context = new DBContext())
            {
                bool IsValidUser = context.Users.Any(user => user.UserName.ToLower() == model.UserName.ToLower() && user.UserPassword == model.UserPassword);

                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("","invalid Username or Password");

                return View();
            }
        }
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(Users model)
        {
            using (DBContext context = new DBContext())
            {
                context.Users.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()

        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}