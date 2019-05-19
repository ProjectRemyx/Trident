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
        public ActionResult Create(string MemberName_New, int MemberLevel_New, string MemberSpecialty_New, int MemberStrikes_New)
        {
            //Query string
            string query = "insert into members (MemberName, MemberLevel, MemberSpecialty, MemberStrikes) values (@name, @level, @specialty, @strikes)";

            //Parameters for the query
            SqlParameter[] myParams = new SqlParameter[4];
            myParams[0] = new SqlParameter("@name", MemberName_New);
            myParams[1] = new SqlParameter("@level", MemberLevel_New);
            myParams[2] = new SqlParameter("@specialty", MemberSpecialty_New);
            myParams[3] = new SqlParameter("@strikes", MemberStrikes_New);
            
            //Execute Query
            db.Database.ExecuteSqlCommand(query, myParams);

            //Re-direct to list of members
            return RedirectToAction("List");
        }

        public ActionResult Show(int? id)
        {
            //If the id doesn't exist or the member doesn't exist
            if((id == null) || (db.Members.Find(id)==null))
            {
                return HttpNotFound();
            }
            string query = "select * from members where memberid=@id";
            SqlParameter[] myParams = new SqlParameter[1];
            myParams[0] = new SqlParameter("@id", id);

            Member myMembers = db.Members.SqlQuery(query, myParams).FirstOrDefault();
            return View(myMembers);
        }

        public ActionResult Edit(int id)
        {
            //Need list of members and the current member
            MemberEdit memberEditView = new MemberEdit();
            memberEditView.member = db.Members.Find(id);
            return View(memberEditView);
        }

        [HttpPost]
        public ActionResult Edit(int id, string MemberName, int MemberLevel, string MemberSpecialty, int MemberStrikes)
        {
            if((id == null) || (db.Members.Find(id) == null))
            {
                return HttpNotFound();
            }
            string query = "update members set MemberName=@name, MemberLevel=@level, MemberSpecialty=@specialty, MemberStrikes=@strikes where MemberID=@id";
            SqlParameter[] myParams = new SqlParameter[5];
            myParams[0] = new SqlParameter("@name", MemberName);
            myParams[1] = new SqlParameter("@level", MemberLevel);
            myParams[2] = new SqlParameter("@specialty", MemberSpecialty);
            myParams[3] = new SqlParameter("@id", id);
            myParams[4] = new SqlParameter("@strikes", MemberStrikes);

            db.Database.ExecuteSqlCommand(query, myParams);
            return RedirectToAction("Show/" + id);
        }

        public ActionResult Delete(int? id)
        {
            if ((id == null) || (db.Members.Find(id) == null))
            {
                return HttpNotFound();
            }
            string query;
            SqlParameter param = new SqlParameter("@id", id);

            //Delete associated characters
            query = "delete from characters where member_MemberID=@id";
            param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
            
            //Delete member
            query = "delete from members where MemberID=@id";
            param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");

        }
        
    }
}