using AutoMapper;
using Notifications.Application.DTOs;
using Notifications.Domain.Entities;

namespace Notifications.Application.Mappings
{
    public class CompanyNotificationMappingProfile : Profile
    {
        public CompanyNotificationMappingProfile()
        {
            CreateMap<CompanyNotificationScheduleAssignRules, CompanyNotificationsScheduleAssignRulesDto>()
                .ForMember(s => s.companyMarketName, o => o.MapFrom(s => $"({s.companyMarket.companyMarketCode}) {s.companyMarket.companyMarketName}"))
                .ForMember(s => s.companyTypeName, o => o.MapFrom(s => $"({s.companyType.companyTypeCode}) {s.companyType.companyTypeName}"))
                .ForMember(s => s.notificationScheduleDays, o => o.MapFrom(s => s.notificationSchedule.days));
        }
    }
}
