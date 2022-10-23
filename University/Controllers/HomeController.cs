using Core.Registration;
using Interface;
using Model;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace University.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRegistrationBL _registrationBL;
        public HomeController()
        {
            _registrationBL = new RegistrationBL();
        }
        public ActionResult Index()
        {
            var loggedUser = Session["CurrentUser"] as User;

            if (loggedUser != null && loggedUser.Role == 0)
            {
                return View("StudentProfile");
            }

            return View();
        }

        public ActionResult StudentProfile()
        {
            var loggedUser = Session["CurrentUser"] as User;
            
        }

        public ActionResult About()
        {
            
            var result = _registrationBL.RegisterUser(new User());
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}