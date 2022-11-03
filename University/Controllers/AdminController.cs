using Core.StudentManager;
using Core.SubjectManager;
using Interface;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace University.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStudentBL _studentBL;
        private readonly ISubjectBL _subjectBL;

        public AdminController()
        {
            _studentBL = new StudentBL();
            _subjectBL = new SubjectBL();
        }
        public ActionResult AdminHome()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetStudents()
        {
            var subject = await _subjectBL.GetById(1);
            var allstudents = await _studentBL.GetAll();
            if (allstudents == null)return Json(null);
            var markedstudents = await _studentBL.ComputeMarkAndStatus(allstudents.ToList());
            if (markedstudents == null)
            {
                return null;
            }
            return Json(subject, JsonRequestBehavior.AllowGet);
        }
    }
}