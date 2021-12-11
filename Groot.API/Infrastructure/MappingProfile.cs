using System;
using AutoMapper;

namespace Groot.API.Controllers.Infrastructure
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Groot.Model.User.UserViewModel, Groot.DB.Entities.User>();
            CreateMap<Groot.DB.Entities.User, Groot.Model.User.UserViewModel>();
            CreateMap<Groot.Model.Product.ProductViewModel, Groot.DB.Entities.Product>();
            CreateMap<Groot.DB.Entities.Product, Groot.Model.Product.ProductViewModel>();
        }
    }
}
