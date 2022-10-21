using Core.Registration;
using Interface;
using Model;
using System.Web.Mvc;

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
            return View();
        }

        public ActionResult About()
        {
            
            var result = _registrationBL.RegisterUser(new Registration());
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