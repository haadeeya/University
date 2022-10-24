using Core.Registration;
using Core.Student;
using Interface;
using Model;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace University.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserBL _registrationBL;
        private readonly IStudentBL _studentBL;
        public HomeController()
        {
            _registrationBL = new UserBL();
            _studentBL = new StudentBL();
        }
        public ActionResult Index()
        {
            var loggedUser = Session["CurrentUser"] as User;

            if (loggedUser != null && loggedUser.Role == 0)
            {
                var currentStudent = _studentBL.GetbyId(loggedUser.Id);
                return View(currentStudent);
            }

            return View();
        }

        public ActionResult StudentProfile()
        {
            Student currentStudent = new Student();
            var loggedUser = Session["CurrentUser"] as User;
            if (loggedUser != null) { 
                
            }
            return View(currentStudent);
        }

        public ActionResult About()
        {
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