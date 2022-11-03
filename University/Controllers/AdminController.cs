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

        public AdminController()
        {
            _studentBL = new StudentBL();
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
            var markedstudents = await _studentBL.ComputeMarkAndStatusAsync(allstudents.ToList());
            if (markedstudents == null)
            {
                return null;
            }
            return Json(markedstudents, JsonRequestBehavior.AllowGet);
        }
    }
}