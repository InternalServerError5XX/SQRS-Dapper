using AutoMapper;
using Relations.Application.Services.UserService;
using Relations.Domain.DTO.CommentDto;
using Relations.Domain.DTO.PostDto;
using Relations.Domain.DTO.ProfileDto;
using Relations.Domain.DTO.UserDto;
using Relations.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Domain.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RequestPostDto, Post>()
                .ForMember(dest => dest.Date,
                    opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<Post, ResponsePostDto>();

            CreateMap<ApplicationUser, PostUserDto>();

            CreateMap<RequestCommentDto, Comment>()
                .ForMember(dest => dest.Date,
                    opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<Comment, ResponseCommentDto>();

            CreateMap<RequestProfileDto, UserProfile>()
                .ForMember(dest => dest.Date,
                    opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<UserProfile, ResponseProfileDto>();

            CreateMap<UserProfile, PostProfileDto>();
            CreateMap<UserProfile, ResponseProfileDto>();
        }
    }
}
