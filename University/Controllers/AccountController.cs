using Core.Registration;
using Interface;
using Model;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using University.Utility;

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
        public async Task<ActionResult> Login(Login login)
        {
            MyLogger.GetInstance().Info("Entering the DAL for login");
            if (ModelState.IsValid)
            {
                var user = await _userBL.Authenticate(login);
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