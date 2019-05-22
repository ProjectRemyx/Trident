﻿using System;
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
    public class TeamController : Controller
    {
        private MemberContext db = new MemberContext();

        //Run method when no command - Plural team view
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            //Print out a list of teams
            IEnumerable<Team> teams = db.Teams.ToList();
            return View(teams);
        }

        public ActionResult New()
        {
            //Connect to db to get list of teams
            TeamEdit teamEditView = new TeamEdit();
            teamEditView.Members = db.Members.ToList();
            return View(teamEditView);
        }

        [HttpPost]
        public ActionResult Create(string TeamName_New, string TeamRep_New, string TeamType_New)
        {
            //Query string
            string query = "insert into teams (TeamName, TeamRep, TeamType) values (@name, @rep, @type)";

            //Parameters for the query
            MySqlParameter[] myParams = new MySqlParameter[3];
            myParams[0] = new MySqlParameter("@name", TeamName_New);
            myParams[1] = new MySqlParameter("@rep", TeamRep_New);
            myParams[2] = new MySqlParameter("@type", TeamType_New);

            //Execute Query
            db.Database.ExecuteSqlCommand(query, myParams);

            //Re-direct to list of members
            return RedirectToAction("List");
        }

        public ActionResult Show(int? id)
        {
            //If the id doesn't exist or the member doesn't exist
            if ((id == null) || (db.Teams.Find(id) == null))
            {
                return HttpNotFound();
            }
            string query = "select * from teams where teamid=@id";
            MySqlParameter[] myParams = new MySqlParameter[1];
            myParams[0] = new MySqlParameter("@id", id);

            Team myTeams = db.Teams.SqlQuery(query, myParams).FirstOrDefault();
            return View(myTeams);
        }

        public ActionResult Edit(int id)
        {
            //Need list of teams and the current team
            TeamEdit teamEditView = new TeamEdit();
            teamEditView.Members = db.Members.ToList();
            teamEditView.Team = db.Teams.Find(id);
            return View(teamEditView);
        }

        [HttpPost]
        public ActionResult Edit(int id, string TeamName, string TeamRep, string TeamType)
        {
            if ((id == null) || (db.Teams.Find(id) == null))
            {
                return HttpNotFound();
            }
            string query = "update teams set TeamName=@name, TeamRep=@rep, TeamType=@type where TeamID=@id";
            MySqlParameter[] myParams = new MySqlParameter[4];
            myParams[0] = new MySqlParameter("@name", TeamName);
            myParams[1] = new MySqlParameter("@rep", TeamRep);
            myParams[2] = new MySqlParameter("@type", TeamType);
            myParams[3] = new MySqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, myParams);
            return RedirectToAction("Show/" + id);
        }

        public ActionResult Delete(int? id)
        {
            if ((id == null) || (db.Teams.Find(id) == null))
            {
                return HttpNotFound();
            }
            string query;
            MySqlParameter param = new MySqlParameter("@id", id);

            /* 
             Structure taken from MemberController where all of the members characters would be deleted.
             Not sure if this is needed for teams as I wish to keep members and asign them to new teams.
             */
            ////Delete associated characters
            //query = "delete from characters where member_MemberID=@id";
            //param = new SqlParameter("@id", id);
            //db.Database.ExecuteSqlCommand(query, param);

            //Delete team
            query = "delete from teams where TeamID=@id";
            param = new MySqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");

        }

    }
}