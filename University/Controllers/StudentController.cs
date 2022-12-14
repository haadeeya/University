using Core.Registration;
using Core.StudentManager;
using Core.SubjectManager;
using Interface;
using Interface.Repository;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace University.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentBL _studentBL;
        private readonly ISubjectBL _subjectBL;

        public StudentController(IStudentBL studentBL, ISubjectBL subjectBL)
        {
            _studentBL = studentBL;
            _subjectBL = subjectBL;
        }

        public async Task<ActionResult> Index()
        {
            var loggedUser = (User)Session["CurrentUser"];

            if (loggedUser != null && loggedUser.Role == 0)
            {
                var currentStudent = await _studentBL.GetByIdAsync(loggedUser.Id);
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
            var student = await _studentBL.GetByIdAsync(loggedUser.Id);
            return Json(student, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateProfile(Student student)
        {
            if (!ModelState.IsValid) return Json(new {result=false, allErrors = ModelState.Values.SelectMany(v => v.Errors) }, JsonRequestBehavior.AllowGet);
            
            var loggedUser = (User)Session["CurrentUser"];
            student.StudentId = loggedUser.Id;
            student.UserId = loggedUser.Id;
            foreach(var subject in student.Subjects)
            {
                subject.StudentId = student.StudentId;
            }
            var newstudent = await _studentBL.CreateAsync(student);
            if (newstudent == null)return null;
            return Json(new { result = true, url = Url.Action("Index", "Student") });

        }

        [HttpGet]
        public async Task<JsonResult> GetSubjects()
        {
            var subjectslist =  await _subjectBL.GetAllAsync();
            return Json(subjectslist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StudentProfile()
        {
            return View();
        }
    }
}