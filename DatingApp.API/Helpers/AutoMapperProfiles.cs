using AutoMapper; 
using DatingApp.API.Models;
using DatingApp.API.Dtos;
using System.Linq;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember( destination => destination.PhotoUrl,
                    opt => opt.MapFrom(source => source.Photos.FirstOrDefault(
                    p => p.IsMain).Url) )
                .ForMember( destination => destination.Age,
                    opt => opt.MapFrom(source => source.DateOfBirth.CalculateAge()) );
            CreateMap<User, UserForDetailedDto>()
                .ForMember( destination => destination.PhotoUrl,
                    opt => opt.MapFrom(source => source.Photos.FirstOrDefault(
                    p => p.IsMain).Url) )
                .ForMember( destination => destination.Age,
                    opt => opt.MapFrom(source => source.DateOfBirth.CalculateAge()) );
            CreateMap<Photo, PhotosForDetailedDto>();
        }
    }
}