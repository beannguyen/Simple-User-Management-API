using AutoMapper;
using Simple_User_Management_API.Models;
using Simple_User_Management_API.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_User_Management_API.Mappers
{
    public class RegisterModelToUserProfile : Profile
    {
        public RegisterModelToUserProfile()
        {
            CreateMap<RegisterModel, User>()
                .ForMember(des => des.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(des => des.Password, opts => opts.MapFrom(src => src.Password));
        }
    }
}
