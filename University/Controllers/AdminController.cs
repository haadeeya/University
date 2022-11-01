﻿using Core.StudentManager;
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
        public async Task<ActionResult> AdminHome()
        {
            var allstudents = await _studentBL.GetAll();

            return View(allstudents);
        }
    }
}