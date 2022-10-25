using Core.Registration;
using Core.StudentManager;
using Interface;
using Interface.Repository;
using Model;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace University.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserBL _userBL;
        private readonly IRepositoryBL<Student> _studentBL;
        public HomeController()
        {
            _userBL = new UserBL();
            _studentBL = new StudentBL();
        }
        public async Task<ActionResult> Index()
        {
            var loggedUser = Session["CurrentUser"] as User;

            if (loggedUser != null && loggedUser.Role == 0)
            {
                var currentStudent = await _studentBL.GetbyId(loggedUser.Id);
                if(currentStudent == null)
                {
                    return RedirectToAction("CreateProfile");
                }
                else
                {
                    return View(currentStudent);
                }
            }

            return View();
        }

        public ActionResult CreateProfile()
        {
            return View();
        }

        public ActionResult StudentProfile()
        {
            return View();
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