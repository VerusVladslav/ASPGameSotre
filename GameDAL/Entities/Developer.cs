using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDAL.Entities
{
    public class Developer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; }

        public Developer()
        {
            Games = new List<Game>();
        }
    }
}
