using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trident.Models.ViewModels
{
    public class TeamEdit
    {
    //Empty Constructor
    public TeamEdit()
    {

    }

    //To edit a team you need to pick from a list of teams
    public virtual Team team { get; set; }

    }
}