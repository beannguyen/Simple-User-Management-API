using AutoMapper;
using Simple_User_Management_API.Models;
using Simple_User_Management_API.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_User_Management_API.Mappers
{
    public class UpdateProfileModelToUserProfile:Profile
    {
        public UpdateProfileModelToUserProfile()
        {
            CreateMap<UpdateProfileModel, User>()
               .ForMember(des => des.FirstName, opts => opts.MapFrom(src => src.FirstName))
               .ForMember(des => des.LastName, opts => opts.MapFrom(src => src.LastName))
               .ForMember(des => des.ProfilePicture, opts => opts.MapFrom(src => src.ProfilePicture));
        }
    }
}
