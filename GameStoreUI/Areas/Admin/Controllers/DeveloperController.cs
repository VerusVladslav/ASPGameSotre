using AutoMapper;
using GameBLL.Services;
using GameBLL.Services.Abstraction;
using GameDAL.Entities;
using GameDAL.Repository.Abstraction;
using GameStoreUI.Models.VIewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStoreUI.Areas.Admin.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class DeveloperController : Controller
    {
        private readonly IGameService gameService;
        private readonly IMapper mapper;
        // GET: Game
    
        public DeveloperController(IGameService service, IMapper map)
        {
            gameService = service;
            mapper = map;
        }
        // GET: Developer
        public ActionResult Index()
        {
            var devs = gameService.GetDevelopers();
            var Viewdevs = mapper.Map<ICollection<DeveloperView>>(devs).ToList();
           
            return View(Viewdevs);
        }
        [HttpGet]
        public ActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Create(DeveloperView view)
        {
            if (ModelState.IsValid)
            {
                var dev = mapper.Map<Developer>(view);
                gameService.AddDeveloper(dev);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {


            var dev = gameService.GetDeveloper(id);
            return View(mapper.Map<DeveloperView>(dev));
        }



        [HttpPost]
        public ActionResult Edit(DeveloperView model)
        {

            if (ModelState.IsValid)
            {
                var developer = mapper.Map<Developer>(model);
                gameService.Update(developer);

                return RedirectToAction("Index");
            }

            return Edit(model.Id);


        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            gameService.DeleteDeveloper(id);
          //  gameService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}