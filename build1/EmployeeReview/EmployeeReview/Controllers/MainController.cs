using EmployeeReview.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeReview.Controllers
{
    public class MainController : Controller
    {
        DesignationsController apii = new DesignationsController();
        // GET: Main
        public ActionResult Index()
        {
            ViewBag.designations = new SelectList(apii.GetDesignations(), "DesignationID", "Designation1");
            Session["test"] = "qqqqqqqqqqqqqqqq";
            return View("LoginView");
        }
        public ActionResult Login(string name)
        {
            Session["name"] = name;
            if (true)
            {
                return View("SkillTypeView");
            }
            else
            {
                return View("Summary");
            }
        }

        public ActionResult Summary()
        {
            return View("SummaryView");
        }

        public ActionResult Skills()
        {
            
            return View("SkillTypeView");
        }

        public ActionResult SkillType(int skillType)
        {
            Session["skillType"] = skillType;
            if (true)
            {
                return View("SkillView");
            }
            else
            {
                return View("SummaryView");
            }
        }

    }
}