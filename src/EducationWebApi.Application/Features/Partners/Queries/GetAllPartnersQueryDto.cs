using AutoMapper;
using EducationWebApi.Application.Common.Mappings;
using EducationWebApi.Application.Helpers;
using EducationWebApi.Core.Entities;

namespace EducationWebApi.Application.Features;

public class GetAllPartnersQueryDto : IMapFrom<Partners>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Partners, GetAllPartnersQueryDto>()
            .ForMember(d => d.Image, opt => opt.MapFrom(s => StorageManager.GetUrl(s.PartnersImage.Storage, s.PartnersImage.Path)));
    }
}