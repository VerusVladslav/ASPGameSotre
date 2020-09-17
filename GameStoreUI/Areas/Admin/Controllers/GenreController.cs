using AutoMapper;
using GameBLL.Services.Abstraction;
using GameDAL.Entities;
using GameStoreUI.Models.VIewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStoreUI.Areas.Admin.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class GenreController : Controller
    {
        private readonly IGameService gameService;
        private readonly IMapper mapper;
        // GET: Game
      
        public GenreController(IGameService service, IMapper map)
        {
            gameService = service;
            mapper = map;
        }
        // GET: Genre
        public ActionResult Index()
        {
            var genr = gameService.GetGenres();
            var ViewGenre = mapper.Map<ICollection<GenreView>>(genr).ToList();

            
            return View(ViewGenre);

        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {


            var genre = gameService.GetGenre(id);
            return View(mapper.Map<GenreView>(genre));
        }



        [HttpPost]
        public ActionResult Edit(GenreView model)
        {

            if (ModelState.IsValid)
            {
                var genre = mapper.Map<Genre>(model);
                gameService.Update(genre);

                return RedirectToAction("Index");
            }

            return Edit(model.Id);


        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            gameService.DeleteGenre(id);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Create(GenreView view)
        {
            if (ModelState.IsValid)
            {
                var dev = mapper.Map<Genre>(view);
                gameService.AddGenre(dev);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}