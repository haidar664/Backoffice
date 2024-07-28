using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Backoffice.Data;
using Backoffice.Models;
using Backoffice.Dtos;

namespace Backoffice.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserCreationDto, AppUser>();
            CreateMap<AppUser, UserDto>();
            CreateMap<EventDto, EventModel>();
        }
    }
}
