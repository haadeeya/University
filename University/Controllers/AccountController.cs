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

        public AccountController(IUserBL userBL)
        {
            _userBL = userBL;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Login(Login data)
        {
            MyLogger.GetInstance().Info("Entering the Account Controller for login");

            if (!ModelState.IsValid)
            {
                return Json(new { result = false, error = "Invalid data" }, JsonRequestBehavior.AllowGet);
            }

            User user = await _userBL.Authenticate(data);

            if (user == null)
            {
                return Json(new { result = false, error = "Password and username do not match or user does not exist." }, JsonRequestBehavior.AllowGet); ; ;
            }

            Session["CurrentUser"] = user;

            if (user.Role == Role.Admin)
            {
                return Json(new { url = Url.Action("AdminHome", "Admin") }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { url = Url.Action("Index", "Student") }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { error = "Invalid data" }, JsonRequestBehavior.AllowGet);
            }

            if (await _userBL.GetByUsername(user.Username) != null)
            {
                return Json(new { error = "Username already exists" }, JsonRequestBehavior.AllowGet);
            }

            user = await _userBL.CreateAsync(user);

            if (user != null)
            {
                User newUser = await _userBL.GetByUsername(user.Username);

                if (newUser != null)
                {
                    Session["CurrentUser"] = new User
                    {
                        Id = newUser.Id,
                        Username = newUser.Username,
                        Password = string.Empty,
                        Role = newUser.Role,
                        Email = newUser.Email
                    };

                    return Json(new { url = Url.Action("Index", "Student") }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { error = "Please enter your details properly." }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();

            return RedirectToAction("Login");
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}