﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trident.Models;

namespace Trident.Controllers
{
    public class MemberController : Controller
    {
        
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }
    }
}