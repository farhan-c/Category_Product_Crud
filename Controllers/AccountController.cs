using System;
using System.Web;
using System.Web.Mvc;
using Crud_Operation.Models;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections;

namespace Crud_Operation.Controllers
{
    public class AccountController : Controller
    {
        private CrudDbContext db = new CrudDbContext();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(Account acc)
        {
            bool isvalid = await db.Accounts.AnyAsync(x => x.Username == acc.Username && x.Password == acc.Password);
            if (isvalid)
            {
                //FormsAuthentication.SetAuthCookie(acc.Username, false);
                var user = acc.Username;
                var token = JwtAuthentication.CreateJWTToken(acc);
                Response.Cookies.Set(new HttpCookie("token", token));
                //db.Accounts.Add(acc);

                return RedirectToAction("CategoryList", "Category", new { createdby = user });
            }
            ModelState.AddModelError("", "Invalid Username or Password");
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Signup(Account acc)
        {
            db.Accounts.Add(acc);
            await db.SaveChangesAsync();
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            if (Request.Cookies["token"] != null)
            {
                var c = new HttpCookie("token")
                {
                    Expires = DateTime.Now.AddSeconds(1)
                };
                Response.Cookies.Add(c);
            }
            return RedirectToAction("Login");

        }
    }
}