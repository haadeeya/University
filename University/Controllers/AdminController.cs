using Core.StudentManager;
using Interface;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace University.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStudentBL _studentBL;

        public AdminController(IStudentBL studentBL)
        {
            _studentBL = studentBL;
        }

        public ActionResult AdminHome()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetStudents()
        {
            var allstudents = await _studentBL.GetAllAsync();
            if (allstudents == null)return Json(null);
            var markedstudents = await _studentBL.ComputeMark(allstudents.ToList());
            return Json(markedstudents, JsonRequestBehavior.AllowGet);
        }
    }
}