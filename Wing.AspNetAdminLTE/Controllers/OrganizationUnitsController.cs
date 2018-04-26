using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Wing.AspNetAdminLTE.Controllers
{
    public class OrganizationUnitsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}