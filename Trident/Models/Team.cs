using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Trident.Models
{
    public class Team
    {
        //One Team has many Members
        [Key, ScaffoldColumn(false), Required]
        public int TeamID { get; set; }

        [Required, StringLength(30, ErrorMessage = "Name cannot be longer than 30 characters.")]
        public string TeamName { get; set; }

        [Required, StringLength(30, ErrorMessage = "Role cannot be longer than 30 characters.")]
        public string TeamRep { get; set; }

        [Required, StringLength(30, ErrorMessage = "Type cannot be longer than 30 characters.")]
        public string TeamType { get; set; }

        [Required]
        public int TeamMembers { get; set; }

        //Represent one team has multiple members
        public virtual ICollection<Member> Members { get; set; }
    }
}