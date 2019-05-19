using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;
using Trident.Models;
using Trident.Models.ViewModels;
using System.Diagnostics;

namespace Trident.Controllers
{
    public class CharacterController : Controller
    {
        //Db variable
        private MemberContext db = new MemberContext();
        // GET: Character
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult New()
        {
            return View(db.Members.ToList());
        }
        
        [HttpPost]
        public ActionResult Create(string CharacterName_New, int CharacterWeapon_New, int CharacterTreasure_New, int? CharacterMember_New)
        {
            //Query string 
            string query = "insert into characters(CharacterName, CharacterWeapon, CharacterTreasure, member_MemberID) values (@name, @weapon, @treasure, @mid)";

            SqlParameter[] myParams = new SqlParameter[4];
            myParams[0] = new SqlParameter("@name", CharacterName_New);
            myParams[1] = new SqlParameter("@weapon", CharacterWeapon_New);
            myParams[2] = new SqlParameter("@treasure", CharacterTreasure_New);
            myParams[3] = new SqlParameter("@mid", CharacterMember_New);

            db.Database.ExecuteSqlCommand(query, myParams);
            return RedirectToAction("List");
        }

        public ActionResult Show(int id)
        {
            string query = "select * from characters where characterid = @id";
            return View(db.Characters.SqlQuery(query, new SqlParameter("@id", id)).FirstOrDefault());
        }

        public ActionResult List()
        {
            return View(db.Characters.ToList());
        }

        public ActionResult Edit(int? id)
        {
            CharacterEdit characterEdit = new CharacterEdit();
            characterEdit.character = db.Characters.Find(id);
            characterEdit.members = db.Members.ToList();
            
            if(characterEdit.character != null)
            {
                return View(characterEdit);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Edit(int? id, string CharacterName, int CharacterWeapon, int CharacterTreasure, int? CharacterMember)
        {
            if ((id == null) || (db.Characters.Find(id) == null))
            {
                return HttpNotFound();
            }

            string query = "update characters set CharacterName=@name, " +
                "CharacterWeapon=@weapon,"+
                "CharacterTreasure=@treasure," +
                "member_MemberID=@mid where CharacterID=@id";

            SqlParameter[] myParams = new SqlParameter[5];
            myParams[0] = new SqlParameter();
            myParams[0].ParameterName = "@name";
            myParams[0].Value = CharacterName;

            myParams[1] = new SqlParameter();
            myParams[1].ParameterName = "@weapon";
            myParams[1].Value = CharacterWeapon;

            myParams[2] = new SqlParameter();
            myParams[2].ParameterName = "@treasure";
            myParams[2].Value = CharacterTreasure;

            myParams[3] = new SqlParameter();
            myParams[3].ParameterName = "mid";
            myParams[3].Value = CharacterMember;

            myParams[4] = new SqlParameter();
            myParams[4].ParameterName = "@id";
            myParams[4].Value = id;

            db.Database.ExecuteSqlCommand(query, myParams);

            return RedirectToAction("Show/" + id);

        }

        public ActionResult Delete(int id)
        {
            string query = "delete from characters where characterid = @id";
            db.Database.ExecuteSqlCommand(query, new SqlParameter("@id", id));

            //How to redirect to another controller referenced from the following link
            //https://stackoverflow.com/questions/10785245/redirect-to-action-in-another-controller
            return RedirectToAction("List", "Member", new { area = "" });
        }

    }
}