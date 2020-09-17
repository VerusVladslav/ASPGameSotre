using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStoreCoursework.Models.MyModels
{
    public class GameCreateViewModel
    {

        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Input the name  of  game", AllowEmptyStrings = false)]
        [MinLength(2)]
        [StringLength(100)]
        //  [RegularExpression("")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Input the year  of  game")]
        [Display(Name = "Year")]
        [Range(1920, 2020, ErrorMessage = "Year must be upper 1920s and lower 2020s")]
        public int Year { get; set; }
        [Display(Name = "Desc")]

        public string Description { get; set; }
        [Display(Name = "Image")]

        public string Image { get; set; }

        /*navigation properties*/
        [Required(ErrorMessage = "Input the Genre  of  game")]
        [Display(Name = "Genre")]

        public string Genre { get; set; }

        [Display(Name = "Developer")]
        [Required(ErrorMessage = "Input the Developer  of  game")]

        public string Developer { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Input the price  of the  game")]

        public int Price { get; set; }
    }
}