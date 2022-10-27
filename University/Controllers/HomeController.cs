using Core.Registration;
using Core.StudentManager;
using Core.SubjectManager;
using Interface;
using Interface.Repository;
using Model;
using System.Text.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace University.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserBL _userBL;
        private readonly IRepositoryBL<Student> _studentBL;
        private readonly IRepositoryBL<Subject> _subjectBL;
        public HomeController()
        {
            _userBL = new UserBL();
            _studentBL = new StudentBL();
            _subjectBL = new SubjectBL();
        }
        public ActionResult Index()
        {
            var loggedUser = Session["CurrentUser"] as User;

            if (loggedUser != null && loggedUser.Role == 0)
            {
                var currentStudent =  _studentBL.GetbyId(loggedUser.Id);
                if (currentStudent == null)
                {
                    return Json(new { error = "Student Profile doesn't exist." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return View(currentStudent);
                }
            }

            return View();
        }

        [HttpGet]
        public JsonResult GetStudent()
        {
            var loggedUser = Session["CurrentUser"] as User;
            var student = _studentBL.GetbyId(loggedUser.Id);
            //var json_allsubjects = JsonSerializer.Serialize(subjectslist);
            return Json(student, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateProfile()
        {
            
            return View();
        }

        [HttpGet]
        public JsonResult GetSubjects()
        {
            List<Subject> subjectslist =  _subjectBL.Get().ToList();
            //var json_allsubjects = JsonSerializer.Serialize(subjectslist);
            return Json(subjectslist, JsonRequestBehavior.AllowGet);
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