using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDAL.Entities
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        //NavigationProperty Props
        public ICollection<Game> Games { get; set; }
        public Genre()
        {
            Games = new List<Game>();
        }
    }
}
