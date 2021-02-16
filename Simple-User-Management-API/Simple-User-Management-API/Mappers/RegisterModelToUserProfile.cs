using AutoMapper;
using Simple_User_Management_API.Extension;
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
        private readonly JWTConfig _jWTConfig;
        public RegisterModelToUserProfile(JWTConfig jWTConfig)
        {
            _jWTConfig = jWTConfig;
            CreateMap<RegisterModel, User>()
                    .ForMember(des => des.Email, opts => opts.MapFrom(src => src.Email))
                    .ForMember(des => des.FirstName, opts => opts.MapFrom(src => src.FirstName))
                    .ForMember(des => des.LastName, opts => opts.MapFrom(src => src.LastName))
                    .ForMember(des => des.ProfilePicture, opts => opts.MapFrom(src => src.ProfilePicture))
                    .ForMember(des => des.Password, opts => opts.MapFrom(src => src.Password.HashPassword(_jWTConfig.Secret)));
        }
    }
}
