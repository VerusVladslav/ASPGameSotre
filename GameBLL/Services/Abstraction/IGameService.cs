using GameBLL.Filters;
using GameDAL.Entities;
using GameStoreUI.Models.VIewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBLL.Services.Abstraction
{
    public interface IGameService
    {
        ICollection<Game> GetAllGames();
        ICollection<Game> GetGamesUser();
        void AddGame(Game game);
        ICollection<string> GetAllGenres();
        ICollection<string> GetAllDevelopers();
        Game GetGame(int id);
        Developer GetDeveloper(int id);
        Genre GetGenre(int id);
        void Update(Game game);
        void Delete(int id);
        ICollection<Developer> GetDevelopers();
        ICollection<Genre> GetGenres();
        ICollection<Game> FilterGame(List<GameFilter> gameFilters);
        void AddDeveloper(Developer developer);
        void AddGenre(Genre genre);
        void Update(Developer developer);
        void Update(Genre genre);
        void DeleteDeveloper(int id);
        void DeleteGenre(int id);
        ICollection<Game>GetWhislist(string idUser);
        void AddtoWhislist(string userid, Game game);
        Usermanager GetUser(string id);
        void AddUser(Usermanager id);



    }
}
