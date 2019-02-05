using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Trident.Models
{
    public class Character
    {
        //One member has many characters
        [Key, ScaffoldColumn(false), Required]
        public int CharacterID { get; set; }

        [Required, StringLength(30, ErrorMessage = "Name cannot be longer than 30 characters.")]
        public string CharacterName { get; set; }

        [Required, StringLength(30, ErrorMessage = "Role cannot be longer than 30 characters.")]
        public string CharacterRole { get; set; }

        [Required, StringLength(30, ErrorMessage = "Type cannot be longer than 30 characters.")]
        public string CharacterType { get; set; }

        [Required]
        public int CharacterWeapon { get; set; }

        //Referenced from Christine Bittle's MVC Example:
        //https://bitbucket.org/salamanderburger/http5204-pagescms.git
        //Int acting as boolean to check if member has a picture (Character Avatar)
        public int HasPic { get; set; }

        //Type of image, .jpg/.png/.gif
        public string ImgType { get; set; }

        //Represent one member to many characters
        public virtual Member member { get; set; }
    }
}