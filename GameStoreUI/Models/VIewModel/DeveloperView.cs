using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStoreUI.Models.VIewModel
{
    public class DeveloperView
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Input the name  of  game", AllowEmptyStrings = false)]
        [MinLength(2)]
        [StringLength(100)]
        //  [RegularExpression("")]
        public string Name { get; set; }


    }
}