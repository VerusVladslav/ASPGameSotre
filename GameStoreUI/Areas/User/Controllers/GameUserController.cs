using AutoMapper;
using GameBLL.Filters;
using GameBLL.Services.Abstraction;
using GameDAL.Entities;
using GameStoreUI.Models;
using GameStoreUI.Models.VIewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStoreUI.Areas.User.Controllers
{
    public class GameUserController : Controller
    {
        private  Usermanager user;
        private ApplicationDbContext _context=new ApplicationDbContext();
        private readonly IGameService gameService;
        private readonly IMapper mapper;
        

      
        // GET: Game
        public GameUserController()
        {
            
            


        }
        public GameUserController(IGameService service, IMapper map)
        {
            gameService = service;
            mapper = map;
            _context = new ApplicationDbContext();
            user = new Usermanager();



        }
       
        [HttpGet]
        public ActionResult WishList()
        {
            var userId = User.Identity.GetUserId();
            
            var games = _context.Usermanagers.FirstOrDefault(x => x.UserId == userId).Games.ToList();
            // gameService.GetWhislist(user.UserId);


            var crear = mapper.Map<ICollection<GameCreateViewModel>>(games);
            return View(crear);

           
        }


        [HttpGet]
        public ActionResult Buy(GameCreateViewModel model)
        {
            
            var game = mapper.Map<Game>(model);
            var userId = User.Identity.GetUserId();
             _context.Usermanagers.FirstOrDefault(x => x.UserId == userId).Games.ToList().Add(game);
           
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        
        //public ActionResult Index()
        //{
        //         SetFilters();
        //        var games = gameService.GetAllGames();
        //        var crear = mapper.Map<ICollection<GameCreateViewModel>>(games);
        //        return View(crear);
            
        //}
        
        public ActionResult Index(string type, string name)
        {
            //  =  gameService.GetUser(User.Identity.GetUserId());
        
            SetFilters();
            if (type == null && name == null)
            {


                var games = gameService.GetAllGames();


                var crear = mapper.Map<ICollection<GameCreateViewModel>>(games);
                return View(crear);
            }

            else
            {
                AddFilter(type, name);
                var games = gameService.FilterGame(Session["GameFilters"] as List<GameFilter>).ToList();
                var gameview = mapper.Map<ICollection<GameCreateViewModel>>(games);
                return View(gameview);
            }



        }
        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Filter(string type, string name)
        {
            List<Game> games = null;
            if (type != null && name != null)
            {
                AddFilter(type, name);

                games = gameService.FilterGame(Session["GameFilters"] as List<GameFilter>).ToList();
            }
            else
            {
                games = gameService.GetAllGames().ToList();

            }

            var Viewgames = mapper.Map<ICollection<GameCreateViewModel>>(games);


            return PartialView("Partial/GamePartial", Viewgames);



        }
        [HttpGet]
        public ActionResult Search(string search)
        {
            var games = gameService.GetAllGames().Where(x => x.Name.Contains(search) || x.Developer.Name.Contains(search));
            var gamecView = mapper.Map<ICollection<GameCreateViewModel>>(games);
            if (games.Count() > 0)
            {
                return PartialView("Partial/GamePartial", gamecView);
            }
            return HttpNotFound();
        }
        private void AddFilter(string type, string name)
        {

            if (Session["GameFilters"] == null)
            {
                Session["GameFilters"] = new List<GameFilter>();
            }
            var filters = Session["GameFilters"] as List<GameFilter>;
            var isExist = filters.FirstOrDefault(x => x.Name == name && x.Type == type);
            if (isExist != null)
            {
                filters.Remove(isExist);

                Session["GameFilters"] = filters;
                return;
            }

            var filter = new GameFilter
            {
                Name = name,
                Type = type
            };
            if (type == "Developer")
            {
                filter.Predicate = (x => x.Developer.Name == name);
            }
            if (type == "Genre")
            {
                filter.Predicate = (x => x.Genre.Name == name);
            }
            filters.Add(filter);
            Session["GameFilters"] = filters;
        }

        private void SetFilters()
        {
            ViewBag.Developers = gameService.GetAllDevelopers();
            ViewBag.Genres = gameService.GetAllGenres();
        }










      
    }
}
