using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using Trident.Models;
using Trident.Models.ViewModels;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Data.Entity;

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

        public async Task<ActionResult> List()
        {
            //Print out a list of members
            IEnumerable<Member> members = await db.Members.ToListAsync();
            return View(members);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> New()
        {
            //Connect to db to get list of members
            MemberEdit memberEditView = new MemberEdit();
            memberEditView.Teams = await db.Teams.ToListAsync();
            return View(memberEditView);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Create(string MemberName_New, string MemberLevel_New, string MemberSpecialty_New, int MemberStrikes_New, int? MemberTeam_New)
        {
            if(MemberName_New == "" || MemberLevel_New == "")
            {
                if (MemberName_New == "") TempData["memberNameError"] = MvcHtmlString.Create("Member name required. <br/>");
                if (MemberLevel_New == "") TempData["memberLevelError"] = MvcHtmlString.Create("Member level required. <br/>");

                return RedirectToAction("New");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
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
                        await db.Database.ExecuteSqlCommandAsync(query, myParams);

                        TempData["AddSuccess"] = "Member successfully added";

                        //Re-direct to list of members
                        return RedirectToAction("List");
                    }
                    catch(Exception err)
                    {
                        return View(err.Message);
                    }
                }
                else
                {
                    TempData["AddFail"] = "Failed to add member";
                    //Re-direct to list of members
                    return RedirectToAction("List");
                }
                
            }
        }

        [Authorize(Roles = "Member, Admin")]
        public async Task<ActionResult> Show(int? id)
        {
            //If the id doesn't exist or the member doesn't exist
            if((id == null) || (db.Members.Find(id)==null))
            {
                return HttpNotFound();
            }
            string query = "select * from members where memberid=@id";
            MySqlParameter[] myParams = new MySqlParameter[1];
            myParams[0] = new MySqlParameter("@id", id);

            Member myMembers = await db.Members.SqlQuery(query, myParams).FirstOrDefaultAsync();
            return View(myMembers);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            //Need list of members and the current member
            MemberEdit memberEditView = new MemberEdit();
            memberEditView.Teams = await db.Teams.ToListAsync();
            memberEditView.Member = await db.Members.FindAsync(id);
            return View(memberEditView);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Edit(int id, string MemberName, string MemberLevel, string MemberSpecialty, int MemberStrikes, int? MemberTeam)
        {
            if (MemberName == "" || MemberLevel == "")
            {
                if (MemberName == "") TempData["memberNameError"] = MvcHtmlString.Create("Member name required. <br/>");
                if (MemberLevel == "") TempData["memberLevelError"] = MvcHtmlString.Create("Member level required. <br/>");

                return RedirectToAction("Edit/" + id);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (db.Members.Find(id) == null)
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

                        await db.Database.ExecuteSqlCommandAsync(query, myParams);
                        TempData["EditSuccess"] = "Member successfully edited";
                        return RedirectToAction("Show/" + id);
                    }
                    catch(Exception err)
                    {
                        return View(err.Message);
                    }
                }
                else
                {
                    TempData["EditFail"] = "Failed to edit member";
                    return RedirectToAction("Show/" + id);
                }

            }
        
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if ((id == null) || (db.Members.Find(id) == null))
            {
                return HttpNotFound();
            }
            try
            {
                string deleteCharactersQuery;
                string deleteMemberQuery;

                MySqlParameter param = new MySqlParameter("@id", id);

                //Delete associated characters
                deleteCharactersQuery = "delete from characters where member_MemberID=@id";
                param = new MySqlParameter("@id", id);
                await db.Database.ExecuteSqlCommandAsync(deleteCharactersQuery, param);
            
                //Delete member
                deleteMemberQuery = "delete from members where MemberID=@id";
                param = new MySqlParameter("@id", id);
                await db.Database.ExecuteSqlCommandAsync(deleteMemberQuery, param);

                TempData["DeleteSuccess"] = "Member successfully deleted";
                return RedirectToAction("List");
            }
            catch
            {
                TempData["DeleteFail"] = "Failed to delete member";
                return RedirectToAction("List");
            }
        }
        
    }
}