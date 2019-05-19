using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Trident.Models
{
    public class Member
    {
        //One member has many characters and one team
        [Key, ScaffoldColumn(false), Required]
        public int MemberID { get; set; }

        [Required, StringLength(30, ErrorMessage = "Name cannot be longer than 30 characters.")]
        public string MemberName { get; set; }

        [Required]
        public int MemberLevel { get; set; }

        [Required, StringLength(30, ErrorMessage = "Role cannot be longer than 30 characters.")]
        public string MemberSpecialty { get; set; }

        [Range(0, 3)]
        public int MemberStrikes { get; set; } = 0;

        //Represent one member has multiple characters
        public virtual ICollection<Character> Characters { get; set; }

        //Represent one team to many members
        public virtual Team team { get; set; }
    }
}