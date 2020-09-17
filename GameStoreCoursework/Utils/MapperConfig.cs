using AutoMapper;
using GameDAL.Entities;
using GameStoreCoursework.Models.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStoreCoursework.Utils
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Game, GameCreateViewModel>()
                .ForMember(x => x.Genre, y => y.MapFrom(z => z.Genre.Name))
                .ForMember(x => x.Developer, y => y.MapFrom(z => z.Developer.Name));


            CreateMap<GameCreateViewModel, Game>()
             .ForMember(x => x.Genre, y => y.MapFrom(z => new Genre { Name = z.Genre }))
             .ForMember(x => x.Developer, y => y.MapFrom(z => new Developer { Name = z.Developer }));

            CreateMap<Developer, DeveloperView>();
            CreateMap<DeveloperView, Developer>();

            CreateMap<Genre, GenreView>();
            CreateMap<GenreView, Genre>();
        }
    }
}