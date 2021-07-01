using Data.Entities;
using AutoMapper;
using DTOS.Dto;
using Repo;
using DTOS.UserDtos;
using DTOS;
using System.Linq;

namespace Service
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            
            CreateMap<CreateUserDto, User>()
                .ForMember(src => src.UserName, opt => opt.MapFrom(dst => dst.Email));
            CreateMap<OutputRefreshTokenDto, OutputSignInUserDto>().ReverseMap();
            CreateMap<Role, OutPutRoleDto>().ReverseMap();
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();
            CreateMap<User, OutputUserDto>()
                 .ForMember(src => src.Roles, opt => opt.MapFrom(dst => dst.UserRoles.Select(R=>R.RoleId)));
           
            CreateMap<UpdateUserDto, User>()
                 .ForMember(src => src.UserName, opt => opt.MapFrom(dst => dst.Email));


          
          

       


        }
    }
}
