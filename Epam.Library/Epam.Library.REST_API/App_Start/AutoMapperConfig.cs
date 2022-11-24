using AutoMapper;
using Epam.Library.Entities;
using Epam.Library.REST_API.Models.Books;
using Epam.Library.REST_API.Models.PaperIssues;
using Epam.Library.REST_API.Models.Papers;
using Epam.Library.REST_API.Models.Patents;
using Epam.Library.REST_API.Models.Resources;
using EPAM.Library.REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Library.REST_API
{
    public static class AutoMapperConfig
    {
        public static Mapper Mapper { get; private set; }
        public static void RegisterMaps()
        {

            var config = new MapperConfiguration(cfg =>
            {
                //Resource
                cfg.CreateMap<InformationResource, ResourceCatalogModel>()
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.MapFrom(r => r.GetType().Name));

                //Book
                cfg.CreateMap<CreateBookModel, Book>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(i => Guid.NewGuid()))
                .ForMember(dest => dest.Authors, opt => opt.Ignore());

                cfg.CreateMap<UpdateBookModel, Book>()
                .ForMember(dest => dest.Authors, opt => opt.Ignore());

                //Paper
                cfg.CreateMap<CreatePaperModel, Paper>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(i => Guid.NewGuid()));

                cfg.CreateMap<UpdatePaperModel, Paper>();

                //Patent
                cfg.CreateMap<CreatePatentModel, Patent>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(i => Guid.NewGuid()))
                .ForMember(dest => dest.Inventors, opt => opt.Ignore());

                cfg.CreateMap<UpdatePatentModel, Patent>()
                .ForMember(dest => dest.Inventors, opt => opt.Ignore());

                //PaperIssue
                cfg.CreateMap<CreatePaperIssueModel, PaperIssue>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(i => Guid.NewGuid()));

                cfg.CreateMap<UpdatePaperIssueModel, PaperIssue>();

                //User
                cfg.CreateMap<CreateUserVM, User>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(i => Guid.NewGuid()))
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
            });

            Mapper = new Mapper(config);
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}