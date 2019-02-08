using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
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
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            //Print out a list of members
            IEnumerable<Member> members = db.Members.ToList();
            return View(members);
        }

        public ActionResult New()
        {
            //Connect to db to get list of members
            MemberEdit memberEditView = new MemberEdit();
            return View(memberEditView);
        }

        [HttpPost]
        public ActionResult Create(string MemberName_New, int MemberLevel_New, string MemberRole_New, int HasPic_New)
        {
            //Query
            string query = "insert into members (MemberName, MemberLevel, MemberRole, HasPic) values (@name, @level, @role, @pic)";
            SqlParameter[] myParams = new SqlParameter[4];
            myParams[0] = new SqlParameter("@name", MemberName_New);
            myParams[1] = new SqlParameter("@level", MemberLevel_New);
            myParams[2] = new SqlParameter("@role", MemberRole_New);
            myParams[3] = new SqlParameter("@pic", HasPic_New);
            //Execute Query
            db.Database.ExecuteSqlCommand(query, myParams);

            return RedirectToAction("List");
        }
    }
}