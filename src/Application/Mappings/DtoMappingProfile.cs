using AutoMapper;
using Defender.HealthMonitor.Application.DTOs;
using Defender.HealthMonitor.Domain.Entities.AppHealth;
using Defender.HealthMonitor.Domain.Entities.User;

namespace Defender.HealthMonitor.Application.Common.Mappings;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(
                src => src.CreatedDate.Value.ToShortDateString()));

        CreateMap<AppHealth, AppHealthDto>();
    }
}
