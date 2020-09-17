using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDAL.Entities
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }

        public int Price { get; set; }
        /*navigation properties*/
        public Genre Genre { get; set; }
        public Developer Developer { get; set; }
    }
}
