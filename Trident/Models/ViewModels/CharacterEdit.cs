using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trident.Models.ViewModels
{
    public class CharacterEdit
    {
        //Empty Constructor
        public CharacterEdit()
        {

        }

        //To edit a character you need to pick from a list of characters
        public virtual Character character { get; set; }
    }
}