using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trident.Models;
using Trident.Models.ViewModels;

namespace Trident.Controllers
{
    public class MemberController : Controller
    {
        private MemberContext db = new MemberContext();

        //Run method when no command - Plural members view
        public ActionResult Index()
        {
            return RedirectToAction("Members");
        }

        public ActionResult Members()
        {
            //Print out a list of members
            IEnumerable<Member> members = db.Members.ToList();
            return View(members);
        }
    }
}