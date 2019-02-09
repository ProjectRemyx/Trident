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
        public ActionResult Create(string CharacterName_New, string CharacterRole_New, string CharacterType_New, int CharacterWeapon_New)
        {
            //Query string 
            string query = "insert into characters(CharacterName, CharacterRole, CharacterType, CharacterWeapon) values (@name, @role, @type, @weapon)";

            SqlParameter[] myParams = new SqlParameter[4];
            myParams[0] = new SqlParameter("@name", CharacterName_New);
            myParams[1] = new SqlParameter("@role", CharacterRole_New);
            myParams[2] = new SqlParameter("@type", CharacterType_New);
            myParams[3] = new SqlParameter("@weapon", CharacterWeapon_New);

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
    }
}