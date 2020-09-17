using AutoMapper;
using GameBLL.Filters;
using GameBLL.Services;
using GameBLL.Services.Abstraction;
using GameDAL.Entities;
using GameDAL.Repository.Abstraction;
using GameStoreUI.Images.Helper;
using GameStoreUI.Models.VIewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStoreUI.Areas.Admin.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class GameController : Controller
    {
        private readonly IGameService gameService;
        private readonly IMapper mapper;
        // GET: Game
       
        public GameController(IGameService service, IMapper map)
        {
            gameService = service;
            mapper = map;
        }
     
        public ActionResult Index(string type, string name)
        {
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

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Developers = gameService.GetAllDevelopers();
            ViewBag.Genres = gameService.GetAllGenres();


            return View();
        }
        [HttpPost]
        public ActionResult Create(GameCreateViewModel viewModel, HttpPostedFileBase imageFile)
        {

            if (ModelState.IsValid)
            {
                string filename = Guid.NewGuid().ToString() + ".jpg";
                string FullPathImage = Server.MapPath(Config.GameImagePath) + "\\" + filename;

                using (Bitmap bmp = new Bitmap(imageFile.InputStream))
                {
                    var readyImage = ImageWorker.CreateImage(bmp, 450, 450);
                    if (readyImage != null)
                    {
                        readyImage.Save(FullPathImage, ImageFormat.Jpeg);
                        var game = mapper.Map<Game>(viewModel);
                        game.Image = filename;
                        gameService.AddGame(game);
                        return RedirectToAction("Index");
                    }
                }
            }
            return Create();
        }








        [HttpGet]
        public ActionResult Edit(int id)
        {

            SetFilters();
            var game = gameService.GetGame(id);
            return View(mapper.Map<GameCreateViewModel>(game));
        }
        [HttpPost]
        public ActionResult Edit(GameCreateViewModel model)
        {

           if (ModelState.IsValid)
           {
                var game = mapper.Map<Game>(model);
                gameService.Update(game);

                return RedirectToAction("Index");
           }

            return Edit(model.Id);


        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            gameService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}