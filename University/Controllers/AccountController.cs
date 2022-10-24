using Core.Registration;
using Interface;
using Model;
using System.Web.Mvc;
using System.Web.Security;
using Utility;

namespace University.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserBL _userBL;
        public AccountController()
        {
            _userBL = new UserBL();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var user = _userBL.Authenticate(login);
                if (user.Username!=null)
                    this.Session["CurrentUser"] = user;
                    this.Session["Username"] = user.Username;
                return RedirectToAction("Index", "Home");
            }
            return View(login);
        }

        public ActionResult Register()
        {
            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                MyLogger.GetInstance().Info("Entering the Account Controller for Registration");
                var result = _userBL.Create(user);
                return RedirectToAction("Index");
            }
            return View();

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction("Login");
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}