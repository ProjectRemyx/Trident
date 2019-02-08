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
        //One member has many characters
        [Key, ScaffoldColumn(false), Required]
        public int MemberID { get; set; }

        [Required, StringLength(30, ErrorMessage = "Name cannot be longer than 30 characters.")]
        public string MemberName { get; set; }

        [Required]
        public int MemberLevel { get; set; }

        [Required, StringLength(30, ErrorMessage = "Role cannot be longer than 30 characters.")]
        public string MemberRole { get; set; }


        //Referenced from Christine Bittle's MVC Example:
        //https://bitbucket.org/salamanderburger/http5204-pagescms.git
        //Int acting as boolean to check if member has a picture (Preferably member's main character)
        public int HasPic { get; set; }

        //Type of image, .jpg/.png/.gif
        public string ImgType { get; set; }

        //Represent one member has multiple characters
        public virtual ICollection<Character> Characters { get; set; }
    }
}