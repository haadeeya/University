using Core.StudentManager;
using Core.SubjectManager;
using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
            var allstudents = await _studentBL.GetAll();
            if (allstudents == null)
            {
                return null;
            }
            var markedstudents = await _studentBL.ComputeMark(allstudents.ToList());
            if (markedstudents == null)
            {
                return null;
            }
            return Json(markedstudents, JsonRequestBehavior.AllowGet);
        }
    }
}