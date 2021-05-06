using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOMSight.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(int error = 0)
        {
            switch (error)
            {
                case 505:
                    ViewBag.Title = "Unexpected error";
                    ViewBag.Description = "This is very embarrassing, let's hope it does not happen again.";
                    break;

                case 404:
                    ViewBag.Title = "Page Not Found";
                    ViewBag.Description = "The URL that is trying to enter does not exist.";
                    break;

                case 403:
                    ViewBag.Title = "Forbidden";
                    ViewBag.Description = "You are not authorized to log in this application.";
                    break;

                default:
                    ViewBag.Title = "Page Not Found";
                    ViewBag.Description = "Something is wrong...";
                    break;
            }

            return View("Error");
        }
    }
}