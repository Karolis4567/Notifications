using AutoMapper;
using Notifications.Application.DTOs;
using Notifications.Domain.Entities;

namespace Notifications.Application.Mappings
{
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            CreateMap<CompanyCreateDto, Company>()
                .ForMember(s => s.companyId, o => o.MapFrom(s => s.CompanyId))
                .ForMember(s => s.companyNumber, o => o.MapFrom(s => s.CompanyNumber))
                .ForMember(s => s.companyName, o => o.MapFrom(s => s.CompanyName))
                .ForMember(s => s.companyTypeId, o => o.Ignore())
                .ForMember(s => s.companyMarketId, o => o.Ignore())
                .ForMember(s => s.creationDate, o => o.Ignore())
                .ForMember(s => s.updateDate, o => o.Ignore());
                
        }
    }
}
