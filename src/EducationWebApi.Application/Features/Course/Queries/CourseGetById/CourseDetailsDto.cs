using AutoMapper;
using EducationWebApi.Application.Common.Mappings;
using EducationWebApi.Core.Entities;

namespace EducationWebApi.Application.Features;

public class CourseDetailsDto : IMapFrom<CourseDetailsTab>
{
    public string Name { get; set; } = null!;
    public string Type { get; set; } = "default";
    public string? Description { get; set; }
}