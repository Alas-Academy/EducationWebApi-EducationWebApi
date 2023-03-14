using EducationWebApi.Core.Enums;
using EducationWebApi.Domain.Common;
using EducationWebApi.Domain.Entities;

namespace EducationWebApi.Core.Entities;

public class SocialMedia : BaseAuditableEntity
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public SocialMediaPlatform Platform { get; set; }
    public string Handle { get; set; } = null!;
}
