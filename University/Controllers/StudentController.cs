using Core.Registration;
using Core.StudentManager;
using Core.SubjectManager;
using Interface;
using Interface.Repository;
using Model;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace University.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUserBL _userBL;
        private readonly IRepositoryBL<Student> _studentBL;
        private readonly IRepositoryBL<Subject> _subjectBL;
        public StudentController()
        {
            _userBL = new UserBL();
            _studentBL = new StudentBL();
            _subjectBL = new SubjectBL();
        }

        public async Task<ActionResult> Index()
        {
            var loggedUser = Session["CurrentUser"] as User;

            if (loggedUser != null && loggedUser.Role == 0)
            {
                var currentStudent = await _studentBL.GetById(loggedUser.Id);
                if (currentStudent != null)
                {
                    return View(currentStudent);
                }
            }
            return RedirectToAction("CreateProfile");
        }

        [HttpGet]
        public async Task<JsonResult> GetStudent()
        {
            var loggedUser = Session["CurrentUser"] as User;
            var student = await _studentBL.GetById(loggedUser.Id);
            return Json(student, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateProfile()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateProfile(Student student)
        {
            if (ModelState.IsValid)
            {
                var loggedUser = Session["CurrentUser"] as User;
                student.Id = loggedUser.Id;
                student.UserId = loggedUser.Id;
                foreach(var subject in student.Subjects)
                {
                    subject.StudentId = student.Id;
                }
                var newstudent = await _studentBL.Create(student);
                if (newstudent != null)
                {
                    return Json(new { result = true, url = Url.Action("Index", "Student") });
                }
            }
            return null;
        }

        [HttpGet]
        public async Task<JsonResult> GetSubjects()
        {
            var subjectslist =  await _subjectBL.GetAll();
            return Json(subjectslist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StudentProfile()
        {
            return View();
        }
    }
}