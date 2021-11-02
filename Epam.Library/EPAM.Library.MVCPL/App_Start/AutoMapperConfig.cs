using AutoMapper;
using Epam.Library.Entities;
using EPAM.Library.MVCPL.ViewModels.Author;
using EPAM.Library.MVCPL.ViewModels.Book;
using EPAM.Library.MVCPL.ViewModels.Paper;
using EPAM.Library.MVCPL.ViewModels.Patent;
using EPAM.Library.MVCPL.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.Library.MVCPL
{
    public static class AutoMapperConfig
    {
        public static Mapper Mapper { get; private set; }
        public static void RegisterMaps()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //Create
                cfg.CreateMap<CreateUserVM, User>()
            .ForMember(dest => dest.id, opt => opt.Ignore())
            .ForMember(dest => dest.Roles, opt => opt.Ignore());

                cfg.CreateMap<CreatePaperVM, Paper>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

                cfg.CreateMap<CreateBookVM, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Authors, opt => opt.Ignore());

                cfg.CreateMap<CreatePatentVM, Patent>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Inventors, opt => opt.Ignore());

                cfg.CreateMap<CreateAuthorVM, Author>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

                //Card
                cfg.CreateMap<Book, DisplayCardBookVM>();

                cfg.CreateMap<Paper, DisplayCardPaperVM>();

                cfg.CreateMap<Patent, DisplayCardPatentVM>();

                //Edit
                cfg.CreateMap<Paper, EditPaperVM>();

                //User
                cfg.CreateMap<User, DisplayUserVM>();
            });

            Mapper = new Mapper(config);
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<AppUser, DisplayAppUserVM>()
            //        .ForMember(dest => dest.HasPassword, opt => opt.ResolveUsing(src => src.Password != null));

            //    cfg.CreateMap<CreateAppUserVM, AppUser>()
            //        .ForMember(dest => dest.Id, opt => opt.Ignore())
            //        .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
            //        .ForMember(dest => dest.Awards, opt => opt.Ignore());
            //});

            //Mapper.AssertConfigurationIsValid();
        }
    }
}