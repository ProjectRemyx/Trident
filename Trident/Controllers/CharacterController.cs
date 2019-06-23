using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;
using Trident.Models;
using Trident.Models.ViewModels;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace Trident.Controllers
{
    public class CharacterController : Controller
    {
        //Db variable
        private MemberContext db = new MemberContext();
        
        [Authorize(Roles = "Member")]
        // GET: Character
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {
            return View(db.Members.ToList());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(string CharacterName_New, int CharacterWeapon_New, int CharacterTreasure_New, int? CharacterMember_New)
        {
            if (CharacterName_New == "")
            {
                if (CharacterName_New == "") TempData["characterNameError"] = MvcHtmlString.Create("Character name required. <br/>");
                return RedirectToAction("New");

            }
            else
            {
                if (ModelState.IsValid)
                {
                    //Query string 
                    string query = "insert into characters(CharacterName, CharacterWeapon, CharacterTreasure, member_MemberID) values (@name, @weapon, @treasure, @mid)";

                    MySqlParameter[] myParams = new MySqlParameter[4];
                    myParams[0] = new MySqlParameter("@name", CharacterName_New);
                    myParams[1] = new MySqlParameter("@weapon", CharacterWeapon_New);
                    myParams[2] = new MySqlParameter("@treasure", CharacterTreasure_New);
                    myParams[3] = new MySqlParameter("@mid", CharacterMember_New);

                    db.Database.ExecuteSqlCommand(query, myParams);

                    TempData["AddSuccess"] = "Character successfully added";
                    return RedirectToAction("Show/" + CharacterMember_New, "Member");

                }
                else
                {
                    TempData["AddFail"] = "Failed to add character";
                    return RedirectToAction("Show/" + CharacterMember_New, "Member");
                }

            }
        }

        [Authorize(Roles = "Member, Admin")]
        public ActionResult Show(int id)
        {
            string query = "select * from characters where characterid = @id";
            return View(db.Characters.SqlQuery(query, new MySqlParameter("@id", id)).FirstOrDefault());
        }

        [Authorize(Roles = "Member, Admin")]
        public ActionResult List()
        {
            return View(db.Characters.ToList());
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(int? id, string CharacterName, int CharacterWeapon, int CharacterTreasure, int? CharacterMember)
        {
            if (CharacterName == "")
            {
                if (CharacterName == "") TempData["characterNameError"] = MvcHtmlString.Create("Character name required. <br/>");
                return RedirectToAction("Edit/"+ id);

            }
            else
            {
                if (ModelState.IsValid)
                {
                    if ((id == null) || (db.Characters.Find(id) == null))
                    {
                        return HttpNotFound();
                    }

                    string query = "update characters set CharacterName=@name, " +
                        "CharacterWeapon=@weapon,"+
                        "CharacterTreasure=@treasure," +
                        "member_MemberID=@mid where CharacterID=@id";

                    MySqlParameter[] myParams = new MySqlParameter[5];
                    myParams[0] = new MySqlParameter();
                    myParams[0].ParameterName = "@name";
                    myParams[0].Value = CharacterName;

                    myParams[1] = new MySqlParameter();
                    myParams[1].ParameterName = "@weapon";
                    myParams[1].Value = CharacterWeapon;

                    myParams[2] = new MySqlParameter();
                    myParams[2].ParameterName = "@treasure";
                    myParams[2].Value = CharacterTreasure;

                    myParams[3] = new MySqlParameter();
                    myParams[3].ParameterName = "mid";
                    myParams[3].Value = CharacterMember;

                    myParams[4] = new MySqlParameter();
                    myParams[4].ParameterName = "@id";
                    myParams[4].Value = id;

                    db.Database.ExecuteSqlCommand(query, myParams);

                    TempData["EditSuccess"] = "Character successfully edited";
                    return RedirectToAction("Show/" + id);
                
                }
                else
                {
                    TempData["EditFail"] = "Failed to edit character";
                    return RedirectToAction("Show/" + id);
                }

            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, int mid)
        {
            if(ModelState.IsValid)
            {
                string query = "delete from characters where characterid = @id";
                db.Database.ExecuteSqlCommand(query, new MySqlParameter("@id", id));

                //How to redirect to another controller referenced from the following link
                //https://stackoverflow.com/questions/10785245/redirect-to-action-in-another-controller
                TempData["DeleteSuccess"] = "Character successfully deleted";
                return RedirectToAction("Show/" + mid, "Member");

            }
            else
            {
                TempData["DeleteFail"] = "Failed to delete character";
                return RedirectToAction("Show/" + mid, "Member");
            }
        }

    }
}