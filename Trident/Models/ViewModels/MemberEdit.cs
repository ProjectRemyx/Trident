﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trident.Models.ViewModels
{  
    public class MemberEdit
    {
        //Empty Constructor
        public MemberEdit()
        {

        }
        
        //To edit a member need a list of members
        public virtual Member member { get; set; }
    }
}