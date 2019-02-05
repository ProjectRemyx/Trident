using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trident.Controllers
{
    public class CharacterController : Controller
    {
        // GET: Character
        public ActionResult Index()
        {
            return View();
        }
    }
}