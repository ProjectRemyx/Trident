using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using Trident.Models;
using Trident.Models.ViewModels;
using MySql.Data.MySqlClient;

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

        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {
            //Connect to db to get list of members
            MemberEdit memberEditView = new MemberEdit();
            memberEditView.Teams = db.Teams.ToList();
            return View(memberEditView);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(string MemberName_New, int MemberLevel_New, string MemberSpecialty_New, int MemberStrikes_New, int? MemberTeam_New)
        {
            if (ModelState.IsValid)
            {
                //Query string
                string query = "insert into members (MemberName, MemberLevel, MemberSpecialty, MemberStrikes, team_TeamID) values (@name, @level, @specialty, @strikes, @tid)";

                //Parameters for the query
                MySqlParameter[] myParams = new MySqlParameter[5];
                myParams[0] = new MySqlParameter("@name", MemberName_New);
                myParams[1] = new MySqlParameter("@level", MemberLevel_New);
                myParams[2] = new MySqlParameter("@specialty", MemberSpecialty_New);
                myParams[3] = new MySqlParameter("@strikes", MemberStrikes_New);
                myParams[4] = new MySqlParameter("@tid", MemberTeam_New);

                //Execute Query
                db.Database.ExecuteSqlCommand(query, myParams);

                TempData["AddSuccess"] = "Member successfully added";
                //Re-direct to list of members
                return RedirectToAction("List");
            }
            else
            {
                TempData["AddFail"] = "Failed to add member";
                //Re-direct to list of members
                return RedirectToAction("List");
            }
        }

        [Authorize(Roles = "Member, Admin")]
        public ActionResult Show(int? id)
        {
            //If the id doesn't exist or the member doesn't exist
            if((id == null) || (db.Members.Find(id)==null))
            {
                return HttpNotFound();
            }
            string query = "select * from members where memberid=@id";
            MySqlParameter[] myParams = new MySqlParameter[1];
            myParams[0] = new MySqlParameter("@id", id);

            Member myMembers = db.Members.SqlQuery(query, myParams).FirstOrDefault();
            return View(myMembers);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            //Need list of members and the current member
            MemberEdit memberEditView = new MemberEdit();
            memberEditView.Teams = db.Teams.ToList();
            memberEditView.Member = db.Members.Find(id);
            return View(memberEditView);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(int id, string MemberName, int MemberLevel, string MemberSpecialty, int MemberStrikes, int? MemberTeam)
        {
            if (ModelState.IsValid)
            {
                if ((id == null) || (db.Members.Find(id) == null))
                {
                    return HttpNotFound();
                }
                string query = "update members set MemberName=@name, MemberLevel=@level, MemberSpecialty=@specialty, MemberStrikes=@strikes, team_TeamID=@tid where MemberID=@id";
                MySqlParameter[] myParams = new MySqlParameter[6];
                myParams[0] = new MySqlParameter("@name", MemberName);
                myParams[1] = new MySqlParameter("@level", MemberLevel);
                myParams[2] = new MySqlParameter("@specialty", MemberSpecialty);
                myParams[3] = new MySqlParameter("@id", id);
                myParams[4] = new MySqlParameter("@strikes", MemberStrikes);
                myParams[5] = new MySqlParameter("@tid", MemberTeam);

                db.Database.ExecuteSqlCommand(query, myParams);
                TempData["EditSuccess"] = "Member successfully edited";
                return RedirectToAction("Show/" + id);
            }
            else
            {
                TempData["EditFail"] = "Failed to edit member";
                return RedirectToAction("Show/" + id);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if(ModelState.IsValid)
            {
                if ((id == null) || (db.Members.Find(id) == null))
                {
                    return HttpNotFound();
                }
                string query;
                MySqlParameter param = new MySqlParameter("@id", id);

                //Delete associated characters
                query = "delete from characters where member_MemberID=@id";
                param = new MySqlParameter("@id", id);
                db.Database.ExecuteSqlCommand(query, param);
            
                //Delete member
                query = "delete from members where MemberID=@id";
                param = new MySqlParameter("@id", id);
                db.Database.ExecuteSqlCommand(query, param);

                TempData["DeleteSuccess"] = "Member successfully deleted";
                return RedirectToAction("List");
            }
            else
            {
                TempData["DeleteFail"] = "Failed to delete member";
                return RedirectToAction("List");
            }

        }
        
    }
}