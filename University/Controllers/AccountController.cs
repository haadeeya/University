using Core.Registration;
using Interface;
using Model;
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
        public JsonResult Login(Login data)
        {
            MyLogger.GetInstance().Info("Entering the Account Controller for login");

            if (!ModelState.IsValid)
            {
                return Json(new { error = "Invalid data" }, JsonRequestBehavior.AllowGet);
            }

            var user =  _userBL.Authenticate(data);

            if (user == null)
            {
                return Json(new { error = "Password and username do not match or user does not exist." }, JsonRequestBehavior.AllowGet);
            }

            this.Session["CurrentUser"] = user;
            this.Session["Username"] = user.Username;

            return Json(new { url = Url.Action("Index", "Home") }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Register()
        {

            return View();
        }


        [HttpPost]
        public JsonResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { error = "Invalid data" }, JsonRequestBehavior.AllowGet);
            }

            var result =  _userBL.Create(user);

            if(result != null)
            {
                this.Session["CurrentUser"] = user;
                this.Session["Username"] = user.Username;

                return Json(new { url = Url.Action("Index", "Home") }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { error = "Please enter your details properly." }, JsonRequestBehavior.AllowGet);
            }

            

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