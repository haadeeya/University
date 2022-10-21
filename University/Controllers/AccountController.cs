using Core.Registration;
using Interface;
using Model;
using System.Web.Mvc;

namespace University.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRegistrationBL _registrationBL;

        public AccountController()
        {
            _registrationBL = new RegistrationBL();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(Registration registration)
        {
            var result = _registrationBL.RegisterUser(registration);
            return RedirectToAction("Index");

        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}