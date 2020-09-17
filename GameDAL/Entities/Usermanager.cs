using GameDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStoreUI.Models.VIewModel
{
    public class Usermanager
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IEnumerable<Game> Games { get; set; }
        public Usermanager()
        {
            Games = new List<Game>();
        }
    }
}