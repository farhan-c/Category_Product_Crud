using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CategoryNewProject.Models;
using Crud_Operation.Models;

namespace Crud_Operation.Controllers
{
    public class ReportController : Controller
    {
        private CrudDbContext db = new CrudDbContext();
        // GET: Report
        public ActionResult CreateReport()
        {
          var dataresult = db.Database.SqlQuery<ReportView>("_spReportProduct_category_");                
          return View(dataresult);
        }
    }
}