using Core.Login;
using Core.Registration;
using Interface;
using Interface.Login;
using Model;
using System.Web.Mvc;
using System.Web.Security;
using Utility;

namespace University.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRegistrationBL _registrationBL;
        private readonly ILoginBL _loginBL;

        public AccountController()
        {
            _registrationBL = new RegistrationBL();
            _loginBL = new LoginBL();
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
                var user = _loginBL.Authenticate(login);
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
        public ActionResult Register(User registration)
        {
            if (ModelState.IsValid)
            {
                MyLogger.GetInstance().Info("Entering the Account Controller for Registration");
                var result = _registrationBL.RegisterUser(registration);
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